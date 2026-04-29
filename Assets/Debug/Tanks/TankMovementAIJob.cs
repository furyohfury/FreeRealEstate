using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Debugging
{
    [WithAll(typeof(Tank))]
    [WithNone(typeof(Player))]
    [BurstCompile]
    public partial struct TankMovementAIJob : IJobEntity
    {
        public float DeltaTime;

        public void Execute(ref LocalTransform transform, in Entity tank)
        {
            float3 position = transform.Position;
            position.y = (float)tank.Index;

            var angle = (0.5f + noise.cnoise(position / 10f)) * 4.0f * math.PI;
            var dir = float3.zero;
            math.sincos(angle, out dir.x, out dir.z);

            // Update the LocalTransform.
            transform.Position += dir * DeltaTime * 5.0f;
            transform.Rotation = quaternion.RotateY(angle);
        }
    }
}
