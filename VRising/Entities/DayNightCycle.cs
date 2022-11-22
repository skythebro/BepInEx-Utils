namespace Utils.VRising.Entities;

public static class DayNightCycle
{
    public static ProjectM.DayNightCycle GetSingleton()
    {
        return Systems.ServerScriptMapper.Get()._DayNightCycleAccessor.GetSingleton();
    }

    public static ProjectM.TimeOfDay GetTimeOfDay()
    {
        return GetSingleton().TimeOfDay;
    }

    public static bool IsDay()
    {
        return (GetTimeOfDay() == ProjectM.TimeOfDay.Day);
    }
}