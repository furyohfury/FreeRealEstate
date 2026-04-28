using Unity.Entities;
using UnityEngine;

namespace Debugging
{
    public class TankAuthoring : MonoBehaviour
    {
        public GameObject cannon;
        public GameObject turret;
        public class TankBaker : Baker<TankAuthoring>
        {
            public override void Bake(TankAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Tank
                                     {
                                         Cannon = GetEntity(authoring.cannon, TransformUsageFlags.Dynamic),
                                         Turret = GetEntity(authoring.turret, TransformUsageFlags.Dynamic)
                                     });
            }
        }
    }
}
