using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Debugging
{
    public partial struct TankMovementAISystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            Debug.Log("TankMovementAISystem::OnUpdate");

            var deltatime = SystemAPI.Time.DeltaTime;

            // foreach (var (transform, entity) in SystemAPI.Query<RefRW<LocalTransform>>()
            //                                              .WithAll<Tank>()
            //                                              .WithNone<Player>()
            //                                              .WithEntityAccess())
            // {
            //     float3 position = transform.ValueRO.Position;
            //     position.y = (float)entity.Index;
            //
            //     var angle = (0.5f + noise.cnoise(position / 10f)) * 4.0f * math.PI;
            //     var dir = float3.zero;
            //     math.sincos(angle, out dir.x, out dir.z);
            //
            //     // Update the LocalTransform.
            //     transform.ValueRW.Position += dir * deltatime * 5.0f;
            //     transform.ValueRW.Rotation = quaternion.RotateY(angle);
            // }

            new TankMovementAIJob
            {
                DeltaTime = deltatime
            }.ScheduleParallel();
        }
    }
}
