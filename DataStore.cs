using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SelfDevTimer;

public class AppSettings
{
    public int AdjustMinutes = 30;
    public int ThresholdHours = 5;
    public int HighHours = 8;
}

public struct WindowBounds
{
    public bool HasValue;
    public int X, Y, W, H;
    public bool Maximized;
}

public class DataStore
{
    private readonly string _filePath;
    private readonly Dictionary<DateOnly, DayRecord> _records = new();

    public AppSettings Settings { get; } = new();
    public WindowBounds Window;

    public DataStore(string? filePath = null)
    {
        _filePath = filePath ?? Path.Combine(AppContext.BaseDirectory, "selfdev_log.txt");
    }

    public string FilePath => _filePath;

    public void Load()
    {
        _records.Clear();
        if (!File.Exists(_filePath)) return;

        foreach (var raw in File.ReadAllLines(_filePath))
        {
            var line = raw.Trim();
            if (line.Length == 0) continue;

            var parts = line.Split('\t');

            if (parts[0].StartsWith("@", StringComparison.Ordinal))
            {
                ParseMeta(parts);
                continue;
            }

            if (parts.Length < 3) continue;
            if (!DateOnly.TryParseExact(parts[0], "yyyy-MM-dd", out var date)) continue;
            int.TryParse(parts[1], out var dev);
            int.TryParse(parts[2], out var rest);
            _records[date] = new DayRecord(date, Math.Max(0, dev), Math.Max(0, rest));
        }
    }

    private void ParseMeta(string[] parts)
    {
        switch (parts[0])
        {
            case "@adjust" when parts.Length >= 2 && int.TryParse(parts[1], out var m):
                Settings.AdjustMinutes = m; break;
            case "@threshold" when parts.Length >= 2 && int.TryParse(parts[1], out var t):
                Settings.ThresholdHours = t; break;
            case "@high" when parts.Length >= 2 && int.TryParse(parts[1], out var h):
                Settings.HighHours = h; break;
            case "@window" when parts.Length >= 6
                    && int.TryParse(parts[1], out var x) && int.TryParse(parts[2], out var y)
                    && int.TryParse(parts[3], out var w) && int.TryParse(parts[4], out var wh):
                Window = new WindowBounds
                {
                    HasValue = true, X = x, Y = y, W = w, H = wh, Maximized = parts[5] == "1"
                };
                break;
        }
    }

    public void Save() => WriteToFile(BuildSnapshot());

    public void SaveSnapshot(params (DateOnly date, int devDelta, int restDelta)[] overlays)
    {
        var snapshot = BuildSnapshot();
        foreach (var (date, devDelta, restDelta) in overlays)
        {
            if (devDelta == 0 && restDelta == 0) continue;
            if (!snapshot.TryGetValue(date, out var rec))
            {
                rec = new DayRecord(date);
                snapshot[date] = rec;
            }
            rec.DevSeconds = Math.Max(0, rec.DevSeconds + devDelta);
            rec.RestSeconds = Math.Max(0, rec.RestSeconds + restDelta);
        }
        WriteToFile(snapshot);
    }

    private Dictionary<DateOnly, DayRecord> BuildSnapshot()
        => _records.Values.ToDictionary(r => r.Date, r => new DayRecord(r.Date, r.DevSeconds, r.RestSeconds));

    private void WriteToFile(Dictionary<DateOnly, DayRecord> data)
    {
        var sb = new StringBuilder();

        if (Window.HasValue)
            sb.Append($"@window\t{Window.X}\t{Window.Y}\t{Window.W}\t{Window.H}\t{(Window.Maximized ? 1 : 0)}\n");
        sb.Append($"@adjust\t{Settings.AdjustMinutes}\n");
        sb.Append($"@threshold\t{Settings.ThresholdHours}\n");
        sb.Append($"@high\t{Settings.HighHours}\n");

        foreach (var rec in data.Values.OrderBy(r => r.Date))
            sb.Append($"{rec.Date:yyyy-MM-dd}\t{rec.DevSeconds}\t{rec.RestSeconds}\n");

        File.WriteAllText(_filePath, sb.ToString());
    }

    public DayRecord GetOrCreate(DateOnly date)
    {
        if (!_records.TryGetValue(date, out var rec))
        {
            rec = new DayRecord(date);
            _records[date] = rec;
        }
        return rec;
    }

    public int GetDev(DateOnly date) => _records.TryGetValue(date, out var r) ? r.DevSeconds : 0;
    public int GetRest(DateOnly date) => _records.TryGetValue(date, out var r) ? r.RestSeconds : 0;

    public void AddDevSeconds(DateOnly date, int delta)
    {
        var rec = GetOrCreate(date);
        rec.DevSeconds = Math.Max(0, rec.DevSeconds + delta);
    }

    public void AddRestSeconds(DateOnly date, int delta)
    {
        var rec = GetOrCreate(date);
        rec.RestSeconds = Math.Max(0, rec.RestSeconds + delta);
    }

    public IEnumerable<DayRecord> GetPastRecords(DateOnly today)
        => _records.Values.Where(r => r.Date < today).OrderByDescending(r => r.Date);
}
