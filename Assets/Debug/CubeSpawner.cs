using Unity.Entities;

namespace Debugging
{
    struct CubeSpawner : IComponentData
    {
        public Entity CubePrefab;
    }
}