using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;

namespace Utils.VRising.Entities;

public static class ActiveServantMission
{
    // Get the entities of component type ActiveServantMission.
    public static NativeArray<Entity> GetAll()
    {
        var servantMissionsQuery = World.em.CreateEntityQuery(
                ComponentType.ReadWrite<ProjectM.ActiveServantMission>()
            );
        return servantMissionsQuery.ToEntityArray(Allocator.Temp);
    }

    public static DynamicBuffer<ProjectM.ActiveServantMission> GetBuffer(Entity mission)
    {
        return World.em.GetBuffer<ProjectM.ActiveServantMission>(mission);
    }

    public static List<DynamicBuffer<ProjectM.ActiveServantMission>> GetAllBuffers()
    {
        var buffers = new List<DynamicBuffer<ProjectM.ActiveServantMission>>();
        var entities = GetAll();
        foreach (var entity in entities)
        {
            buffers.Add(GetBuffer(entity));
        }
        return buffers;
    }

    public static List<string> GetAllBuffersMissionUIDs()
    {
        var missionUIDs = new List<string>();
        var missions = GetAllBuffers();

        foreach (var missionBuffers in missions)
        {
            foreach (var buffer in missionBuffers)
            {
                missionUIDs.Add(GetMissionUID(buffer));
            }
        }
        return missionUIDs;
    }

    public static List<string> GetBufferMissionUIDs(Entity mission)
    {
        var missionUIDs = new List<string>();
        var missionBuffers = GetBuffer(mission);
        foreach (var buffer in missionBuffers)
        {
            missionUIDs.Add(GetMissionUID(buffer));
        }
        return missionUIDs;
    }

    public static string GetMissionUID(ProjectM.ActiveServantMission mission)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(
            $"{GetMissiontDataId(mission)}-{GetMissionStartTime(mission)}-{GetMissionName(mission)}"
        );
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string GetMissionName(ProjectM.ActiveServantMission mission)
    {
        return Systems.PrefabCollectionSystem.GetPrefabName(mission.MissionID);
    }

    public static float GetMissionLength(ProjectM.ActiveServantMission mission)
    {
        return mission.MissionLength;
    }

    public static long GetMissionLengthTimestamp(ProjectM.ActiveServantMission mission)
    {
        return DateTimeOffset.Now.AddSeconds(GetMissionLength(mission)).ToUnixTimeSeconds();
    }

    public static void SetMissionLength(ref ProjectM.ActiveServantMission mission, float seconds)
    {
        mission.MissionLength = seconds;
    }

    public static double GetMissionStartTime(ProjectM.ActiveServantMission mission)
    {
        return mission.MissionStartTime;
    }

    public static int GetMissiontDataId(ProjectM.ActiveServantMission mission)
    {
        return mission.MissiontDataId;
    }
}
