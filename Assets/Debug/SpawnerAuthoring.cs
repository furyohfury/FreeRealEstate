using Unity.Entities;
using UnityEngine;

namespace Debugging
{
    public class SpawnerAuthoring : MonoBehaviour
    {
        public GameObject CubePrefab;

        class Baker : Baker<SpawnerAuthoring>
        {
            public override void Bake(SpawnerAuthoring authoring)
            {
                Entity entity = GetEntity(authoring, TransformUsageFlags.None);
                AddComponent(entity, new CubeSpawner()
                                     {
                                         CubePrefab = GetEntity(authoring.CubePrefab, TransformUsageFlags.Dynamic)
                                     });
            }
        }
    }
}