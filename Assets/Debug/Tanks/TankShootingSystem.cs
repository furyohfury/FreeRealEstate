using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Debugging
{
    public partial struct TankShootingSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<TankSpawnConfig>();
        }

        private float _timer;

        // [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            Debug.Log("TankShootingSystem::OnUpdate");

            
            _timer -= SystemAPI.Time.DeltaTime;
            if (_timer > 0)
            {
                return;
            }
            _timer = 0.5f; // reset timer

            var tankSpawnConfig = SystemAPI.GetSingleton<TankSpawnConfig>();
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer buffer = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var tuple in SystemAPI.Query<RefRO<Tank>, RefRO<LocalToWorld>, RefRO<URPMaterialPropertyBaseColor>>())
            {
                RefRO<Tank> tank = tuple.Item1;
                RefRO<LocalToWorld> localToWorld = tuple.Item2;
                RefRO<URPMaterialPropertyBaseColor> materialPropertyBaseColor = tuple.Item3;
                Entity ball = buffer.Instantiate(tankSpawnConfig.CannonBallPrefab);
                buffer.SetComponent(ball, materialPropertyBaseColor.ValueRO);
                var cannonTransform = state.EntityManager.GetComponentData<LocalToWorld>(tank.ValueRO.Cannon);
                buffer.SetComponent(ball, LocalTransform.FromPosition(cannonTransform.Position));
                buffer.SetComponent(ball, new CannonBall
                                              {
                                                  Velocity = math.normalize(cannonTransform.Up) * 12.0f
                                              });
            }
        }
    }
}
