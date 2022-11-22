namespace Utils.VRising.Systems;

public static class PrefabCollectionSystem
{
    public static ProjectM.PrefabCollectionSystem Get()
    {
        return Entities.World.world.GetExistingSystem<ProjectM.PrefabCollectionSystem>();
    }

    public static string GetPrefabName(ProjectM.PrefabGUID hashCode)
    {
        var prefabCollectionSystem = Get();
        return prefabCollectionSystem.PrefabNameLookupMap.GetValueOrDefault(hashCode, "NoPrefabName").ToString();
    }
}
