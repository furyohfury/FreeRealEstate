using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Debugging
{
    public partial struct CubeRotationSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var tuple in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>())
            {
                RefRW<LocalTransform> localTransform = tuple.Item1;
                RefRO<RotationSpeed> rotationSpeed = tuple.Item2;
                float3 direction = rotationSpeed.ValueRO.Direction * deltaTime;
                quaternion quaternion = quaternion.Euler(direction);
                localTransform.ValueRW = localTransform.ValueRW.Rotate(quaternion);
            }
        }
    }
}
