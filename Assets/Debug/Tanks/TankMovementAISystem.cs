using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Debugging
{
    public partial struct TankMovementAISystem : ISystem
    {
        private EntityQuery _entityQuery;
        private ComponentTypeHandle<LocalTransform> localTransformHandle;
        private ComponentTypeHandle<Tank> tankHandle;
        private EntityTypeHandle entityType;

        public void OnCreate(ref SystemState state)
        {
            var entityQueryDesc = new EntityQueryDesc
                                  {
                                      All = new ComponentType[]
                                            {
                                                ComponentType.ReadOnly<Tank>(), ComponentType.ReadWrite<LocalTransform>()
                                            },
                                      None = new ComponentType[]
                                             {
                                                 ComponentType.ReadOnly<Player>()
                                             }
                                  };
            _entityQuery = state.GetEntityQuery(entityQueryDesc);
            localTransformHandle = state.GetComponentTypeHandle<LocalTransform>();
            tankHandle = state.GetComponentTypeHandle<Tank>();
            entityType = state.GetEntityTypeHandle();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            Debug.Log("TankMovementAISystem::OnUpdate");

            var deltatime = SystemAPI.Time.DeltaTime;
            localTransformHandle.Update(ref state);
            entityType.Update(ref state);
            tankHandle.Update(ref state);

            state.Dependency = new TankMovementAIJobChunk
                               {
                                   DeltaTime = deltatime,
                                   localTransformHandle = localTransformHandle,
                                   entityType = entityType,
                                   tankHandle = tankHandle
                               }.ScheduleParallel(_entityQuery, state.Dependency);

            // new TankMovementAIJob
            // {
            //     DeltaTime = deltatime
            // }.ScheduleParallel();
        }
    }
}
