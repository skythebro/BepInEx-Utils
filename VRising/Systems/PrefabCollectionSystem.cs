namespace Utils.VRising.Systems;

public static class PrefabCollectionSystem {
    public static ProjectM.PrefabCollectionSystem Get() {
        return Entities.World.world.GetExistingSystem<ProjectM.PrefabCollectionSystem>();
    }

    public static string GetPrefabName(ProjectM.PrefabGUID hashCode) {
        var pcs = Get();
        string name = "Nonexistent";
        if (hashCode.GuidHash == 0) {
            return name;
        }
        try {
            name = pcs.PrefabNameLookupMap[hashCode].ToString();
        } catch {
            name = "NoPrefabName";
        }
        return name;
    }
}
