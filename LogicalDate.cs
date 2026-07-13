using System;

namespace SelfDevTimer;

public static class LogicalDate
{
    public const int DayStartHour = 6;

    public static DateOnly From(DateTime dt)
        => DateOnly.FromDateTime(dt.Hour < DayStartHour ? dt.AddDays(-1) : dt);

    public static DateOnly Today() => From(DateTime.Now);
}
