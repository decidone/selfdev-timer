using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace SelfDevTimer;

public partial class MainForm : Form
{
    private readonly DataStore _store = new();
    private readonly StopwatchEngine _dev = new(Category.Dev);
    private readonly StopwatchEngine _rest = new(Category.Rest);

    private int _thresholdHours;
    private int _highHours;

    private DateOnly _lastShownDay;
    private bool _initializing;

    private System.Windows.Forms.Timer _uiTimer = null!;
    private System.Windows.Forms.Timer _autoSaveTimer = null!;

    public MainForm()
    {
        _initializing = true;
        InitializeComponent();
        TrySetWindowIcon();

        _store.Load();
        _thresholdHours = _store.Settings.ThresholdHours;
        _highHours = _store.Settings.HighHours;
        _adjustMinutes.Value = Math.Clamp(_store.Settings.AdjustMinutes, 0, 6000);
        _thresholdInput.Value = Math.Clamp(_thresholdHours, 0, 24);
        _highInput.Value = Math.Clamp(_highHours, 0, 24);
        _initializing = false;

        RestoreWindowState();
        _lastShownDay = LogicalDate.Today();
        RefreshHistory();
        UpdateButtonStates();
        RefreshDynamic();

        _uiTimer = new System.Windows.Forms.Timer { Interval = 250 };
        _uiTimer.Tick += (_, _) => RefreshDynamic();
        _uiTimer.Start();

        _autoSaveTimer = new System.Windows.Forms.Timer { Interval = 60_000 };
        _autoSaveTimer.Tick += (_, _) => PersistSnapshot();
        _autoSaveTimer.Start();

        FormClosing += OnFormClosing;
    }

    private void DevPlay_Click(object? sender, EventArgs e) => StartEngine(_dev);
    private void DevPause_Click(object? sender, EventArgs e) { _dev.Pause(); UpdateButtonStates(); }
    private void DevStop_Click(object? sender, EventArgs e) => StopEngine(_dev);
    private void RestPlay_Click(object? sender, EventArgs e) => StartEngine(_rest);
    private void RestPause_Click(object? sender, EventArgs e) { _rest.Pause(); UpdateButtonStates(); }
    private void RestStop_Click(object? sender, EventArgs e) => StopEngine(_rest);

    private void DevPlus_Click(object? sender, EventArgs e) => AdjustToday(Category.Dev, (int)_adjustMinutes.Value);
    private void DevMinus_Click(object? sender, EventArgs e) => AdjustToday(Category.Dev, -(int)_adjustMinutes.Value);
    private void RestPlus_Click(object? sender, EventArgs e) => AdjustToday(Category.Rest, (int)_adjustMinutes.Value);
    private void RestMinus_Click(object? sender, EventArgs e) => AdjustToday(Category.Rest, -(int)_adjustMinutes.Value);

    private void AdjustMinutes_ValueChanged(object? sender, EventArgs e)
    {
        if (_initializing) return;
        _store.Settings.AdjustMinutes = (int)_adjustMinutes.Value;
        PersistSnapshot();
    }

    private void ThresholdInput_ValueChanged(object? sender, EventArgs e)
    {
        if (_initializing) return;
        _thresholdHours = (int)_thresholdInput.Value;
        _store.Settings.ThresholdHours = _thresholdHours;
        RefreshHistory();
        PersistSnapshot();
    }

    private void HighInput_ValueChanged(object? sender, EventArgs e)
    {
        if (_initializing) return;
        _highHours = (int)_highInput.Value;
        _store.Settings.HighHours = _highHours;
        RefreshHistory();
        PersistSnapshot();
    }

    private void TrySetWindowIcon()
    {
        try
        {
            using var s = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("SelfDevTimer.app.ico");
            if (s != null) Icon = new Icon(s);
        }
        catch { /* 아이콘을 못 찾아도 실행에는 지장 없음 */ }
    }

    private void RestoreWindowState()
    {
        var wb = _store.Window;
        if (!wb.HasValue) return;

        var bounds = new Rectangle(wb.X, wb.Y, wb.W, wb.H);
        if (!IsOnAnyScreen(bounds)) return;

        StartPosition = FormStartPosition.Manual;
        Bounds = bounds;
        if (wb.Maximized) WindowState = FormWindowState.Maximized;
    }

    private void SaveWindowState()
    {
        Rectangle b = WindowState == FormWindowState.Normal ? Bounds : RestoreBounds;
        _store.Window = new WindowBounds
        {
            HasValue = true, X = b.X, Y = b.Y, W = b.Width, H = b.Height,
            Maximized = WindowState == FormWindowState.Maximized
        };
    }

    private static bool IsOnAnyScreen(Rectangle r)
    {
        foreach (var sc in Screen.AllScreens)
            if (sc.WorkingArea.IntersectsWith(r)) return true;
        return false;
    }

    private void StartEngine(StopwatchEngine engine)
    {
        if (engine == _dev && _rest.IsRunning) _rest.Pause();
        if (engine == _rest && _dev.IsRunning) _dev.Pause();
        engine.Play();
        UpdateButtonStates();
    }

    private void StopEngine(StopwatchEngine engine)
    {
        int secs = engine.Stop();
        if (engine.Category == Category.Dev)
            _store.AddDevSeconds(engine.AttributionDate, secs);
        else
            _store.AddRestSeconds(engine.AttributionDate, secs);

        PersistSnapshot();
        RefreshHistory();
        UpdateButtonStates();
        RefreshDynamic();
    }

    private void AdjustToday(Category cat, int minutes)
    {
        var today = LogicalDate.Today();
        if (cat == Category.Dev) _store.AddDevSeconds(today, minutes * 60);
        else _store.AddRestSeconds(today, minutes * 60);

        PersistSnapshot();
        RefreshHistory();
        RefreshDynamic();
    }

    private void PersistSnapshot()
    {
        SaveWindowState();
        _store.SaveSnapshot(
            (_dev.AttributionDate, _dev.SessionSeconds, 0),
            (_rest.AttributionDate, 0, _rest.SessionSeconds));
    }

    private void RefreshDynamic()
    {
        var today = LogicalDate.Today();
        if (today != _lastShownDay)
        {
            _lastShownDay = today;
            RefreshHistory();
        }

        _devClock.Text = FormatClock(_dev.SessionSeconds);
        _restClock.Text = FormatClock(_rest.SessionSeconds);
        _devClock.ForeColor = _dev.IsRunning ? SystemColors.ControlText : SystemColors.GrayText;
        _restClock.ForeColor = _rest.IsRunning ? SystemColors.ControlText : SystemColors.GrayText;

        int devToday = _store.GetDev(today) + (_dev.AttributionDate == today ? _dev.SessionSeconds : 0);
        int restToday = _store.GetRest(today) + (_rest.AttributionDate == today ? _rest.SessionSeconds : 0);
        _devToday.Text = $"오늘 누적 {FormatHm(devToday)}";
        _restToday.Text = $"오늘 쉰 시간 {FormatHm(restToday)}";
    }

    private void UpdateButtonStates()
    {
        _devPlay.Enabled = !_dev.IsRunning;
        _devPause.Enabled = _dev.IsRunning;
        _restPlay.Enabled = !_rest.IsRunning;
        _restPause.Enabled = _rest.IsRunning;
    }

    private void RefreshHistory()
    {
        var today = LogicalDate.Today();
        _history.BeginUpdate();
        _history.Items.Clear();
        foreach (var rec in _store.GetPastRecords(today))
        {
            var item = new ListViewItem(rec.Date.ToString("yyyy-MM-dd"));
            item.UseItemStyleForSubItems = false;
            var devCell = item.SubItems.Add(FormatHm(rec.DevSeconds));

            double hours = rec.DevSeconds / 3600.0;
            if (hours >= _highHours)
            {
                devCell.ForeColor = Color.Magenta;
                devCell.Font = new Font(_history.Font, FontStyle.Bold);
            }
            else if (hours >= _thresholdHours)
            {
                devCell.ForeColor = Color.Green;
            }
            else
            {
                devCell.ForeColor = Color.Red;
            }

            _history.Items.Add(item);
        }
        _history.EndUpdate();
    }

    private void OnFormClosing(object? sender, FormClosingEventArgs e)
    {
        _uiTimer.Stop();
        _autoSaveTimer.Stop();
        _store.AddDevSeconds(_dev.AttributionDate, _dev.Stop());
        _store.AddRestSeconds(_rest.AttributionDate, _rest.Stop());
        SaveWindowState();
        _store.Save();
    }

    private static string FormatClock(int totalSeconds)
    {
        int h = totalSeconds / 3600;
        int m = (totalSeconds % 3600) / 60;
        int s = totalSeconds % 60;
        return $"{h:00}:{m:00}:{s:00}";
    }

    private static string FormatHm(int totalSeconds)
    {
        int h = totalSeconds / 3600;
        int m = (totalSeconds % 3600) / 60;
        return $"{h}시간 {m}분";
    }
}
