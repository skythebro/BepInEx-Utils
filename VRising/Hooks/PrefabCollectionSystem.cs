using ProjectM;
using Utils.VRising.Entities;

namespace Utils.VRising.Hooks;

public static class PrefabCollectionSystem {
    public static string GetPrefabName(PrefabGUID hashCode) {
        var s = World.Server.GetExistingSystem<ProjectM.PrefabCollectionSystem>();
        string name = "Nonexistent";
        if (hashCode.GuidHash == 0) {
            return name;
        }
        try {
            name = s.PrefabNameLookupMap[hashCode].ToString();
        } catch {
            name = "NoPrefabName";
        }
        return name;
    }
}
