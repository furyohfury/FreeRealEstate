using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Debugging
{
    [BurstCompile]
    public struct TankMovementAIJobChunk : IJobChunk
    {
        public float DeltaTime;
        public ComponentTypeHandle<LocalTransform> localTransformHandle;
        public ComponentTypeHandle<Tank> tankHandle;
        public EntityTypeHandle entityType;

        public void Execute(
            in ArchetypeChunk chunk,
            int unfilteredChunkIndex,
            bool useEnabledMask,
            in v128 chunkEnabledMask)
        {
            NativeArray<LocalTransform> localTransforms = chunk.GetNativeArray(ref localTransformHandle);
            NativeArray<Entity> entities = chunk.GetNativeArray(entityType);

            for (int i = 0, count = chunk.Count; i < count; i++)
            {
                float3 position = localTransforms[i].Position;
                position.y = entities[i].Index;

                var angle = (0.5f + noise.cnoise(position / 10f)) * 4.0f * math.PI;
                var dir = float3.zero;
                math.sincos(angle, out dir.x, out dir.z);

                // Update the LocalTransform.
                var localTransform = localTransforms[i];
                localTransform.Position += dir * DeltaTime * 5.0f;
                localTransforms[i] = localTransform;
            }
        }
    }
}
