using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Debugging
{
    public partial struct SpawnSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<CubeSpawner>();
        }

        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            Entity cubePrefab = SystemAPI.GetSingleton<CubeSpawner>().CubePrefab;
            var cubes = state.EntityManager.Instantiate(cubePrefab, 10, Allocator.Temp);
            var random = new Random(4124);

            foreach (Entity cube in cubes)
            {
                RefRW<LocalTransform> localTransform = SystemAPI.GetComponentRW<LocalTransform>(cube);
                localTransform.ValueRW.Position += random.NextFloat3(new float3(10, 10, 10));
            }
        }
    }
}
