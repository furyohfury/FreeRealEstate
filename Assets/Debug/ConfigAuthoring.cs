using Unity.Entities;
using UnityEngine;

namespace Debugging
{
    public class ConfigAuthoring : MonoBehaviour
    {
        public GameObject tankPrefab;
        public GameObject cannonBallPrefab;
        public int tankCount;
        public class ConfigBaker : Baker<ConfigAuthoring>
        {
            public override void Bake(ConfigAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new TankSpawnConfig
                                     {
                                         TankPrefab = GetEntity(authoring.tankPrefab, TransformUsageFlags.Dynamic),
                                         CannonBallPrefab = GetEntity(authoring.cannonBallPrefab, TransformUsageFlags.Dynamic),
                                         TankCount = authoring.tankCount
                                     });
            }
        }
    }
}