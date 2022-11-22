using Unity.Entities;

namespace Utils.VRising.Entities;

public static class SetTimeOfDayEvent
{
    public static void Add(int minute = 0, int hour = 0, int day = 0, int month = 0, int year = 0)
    {
        create(minute, hour, day, month, year, ProjectM.Network.SetTimeOfDayEvent.SetTimeType.Add);
    }

    public static void Set(int minute = 0, int hour = 0, int day = 0, int month = 0, int year = 0)
    {
        create(minute, hour, day, month, year, ProjectM.Network.SetTimeOfDayEvent.SetTimeType.Set);
    }

    private static void create(int minute, int hour, int day, int month, int year, ProjectM.Network.SetTimeOfDayEvent.SetTimeType type)
    {
        var setTimeEntity = World.em.CreateEntity(
                    ComponentType.ReadOnly<ProjectM.Network.SetTimeOfDayEvent>()
                );
        World.em.SetComponentData<ProjectM.Network.SetTimeOfDayEvent>(
            setTimeEntity,
            new ProjectM.Network.SetTimeOfDayEvent() { Minute = minute, Hour = hour, Day = day, Month = month, Year = year, Type = type }
        );
    }
}