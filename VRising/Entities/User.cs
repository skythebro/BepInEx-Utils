using Unity.Entities;
using Unity.Collections;

namespace Utils.VRising.Entities;

public static class User {
    public static NativeArray<Entity> Get(EntityManager em) {
        var query = em.CreateEntityQuery(
            ComponentType.ReadOnly<ProjectM.Network.User>()
        );
        return query.ToEntityArray(Allocator.Temp);
    }

    public static Entity GetFirstOnlinePlayer(EntityManager em) {
        var userEntities = Entities.User.Get(em);
        foreach (var userEntity in userEntities) {
            var user = em.GetComponentData<ProjectM.Network.User>(userEntity);
            if (user.IsConnected) {
                return userEntity;
            }
        }
        return Entity.Null;
    }
}
