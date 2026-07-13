using System;

namespace SelfDevTimer;

public enum Category { Dev, Rest }

public class StopwatchEngine
{
    public Category Category { get; }
    public bool IsRunning { get; private set; }
    public DateOnly AttributionDate { get; private set; }

    private int _baseSeconds;
    private DateTime _startTime;

    public StopwatchEngine(Category category)
    {
        Category = category;
        AttributionDate = LogicalDate.Today();
    }

    public int SessionSeconds
        => _baseSeconds + (IsRunning ? (int)(DateTime.Now - _startTime).TotalSeconds : 0);

    public void Play()
    {
        if (IsRunning) return;
        if (_baseSeconds == 0)
            AttributionDate = LogicalDate.Today();
        _startTime = DateTime.Now;
        IsRunning = true;
    }

    public void Pause()
    {
        if (!IsRunning) return;
        _baseSeconds += (int)(DateTime.Now - _startTime).TotalSeconds;
        IsRunning = false;
    }

    public int Stop()
    {
        int total = SessionSeconds;
        _baseSeconds = 0;
        IsRunning = false;
        return total;
    }
}
