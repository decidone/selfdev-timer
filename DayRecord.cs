using System;

namespace SelfDevTimer;

public class DayRecord
{
    public DateOnly Date { get; set; }
    public int DevSeconds { get; set; }
    public int RestSeconds { get; set; }

    public DayRecord(DateOnly date, int devSeconds = 0, int restSeconds = 0)
    {
        Date = date;
        DevSeconds = devSeconds;
        RestSeconds = restSeconds;
    }
}
