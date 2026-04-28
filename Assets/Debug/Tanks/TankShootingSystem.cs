using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace Debugging
{
    public partial struct TankShootingSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TankSpawnConfig>();
        }

        private float _timer;

        // [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            _timer -= SystemAPI.Time.DeltaTime;
            if (_timer > 0)
            {
                return;
            }
            _timer = 0.5f; // reset timer

            var tankSpawnConfig = SystemAPI.GetSingleton<TankSpawnConfig>();

            foreach (var tuple in SystemAPI.Query<RefRO<Tank>, RefRO<LocalToWorld>, RefRO<URPMaterialPropertyBaseColor>>())
            {
                RefRO<Tank> tank = tuple.Item1;
                RefRO<LocalToWorld> localToWorld = tuple.Item2;
                RefRO<URPMaterialPropertyBaseColor> materialPropertyBaseColor = tuple.Item3;
                Entity ball = state.EntityManager.Instantiate(tankSpawnConfig.CannonBallPrefab);
                state.EntityManager.SetComponentData(ball, materialPropertyBaseColor.ValueRO);
                RefRW<LocalTransform> ballTransform = SystemAPI.GetComponentRW<LocalTransform>(ball);

                var cannonTransform = state.EntityManager.GetComponentData<LocalToWorld>(tank.ValueRO.Cannon);
                ballTransform.ValueRW.Position = cannonTransform.Position;
                
                state.EntityManager.SetComponentData(ball, new CannonBall
                                                           {
                                                               Velocity = math.normalize(cannonTransform.Up) * 12.0f
                                                           });
            }
        }
    }
}
