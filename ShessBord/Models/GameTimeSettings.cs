using System;

namespace ShessBord.Models;

public class GameTimeSettings
{
    public TimeOnly MainTime { get; }
    public TimeOnly AddTime { get; }

    public GameTimeSettings(TimeOnly mainTime, TimeOnly addTime)
    {
        MainTime = mainTime;
        AddTime = addTime;
    }
}