using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Debugging
{
    public partial struct TankSpawnSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TankSpawnConfig>();
        }

        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            var tankSpawnConfig = SystemAPI.GetSingleton<TankSpawnConfig>();
            var random = new Random(123);

            for (int i = 0, count = tankSpawnConfig.TankCount; i < count; i++)
            {
                float4 randomColor = RandomColor(ref random);
                var colorComponent = new URPMaterialPropertyBaseColor
                                     {
                                         Value = randomColor
                                     };
                Entity tank = state.EntityManager.Instantiate(tankSpawnConfig.TankPrefab);

                if (i == 0)
                {
                    state.EntityManager.AddComponent<Player>(tank);
                }

                var linkedEntities = state.EntityManager.GetBuffer<LinkedEntityGroup>(tank);
                foreach (LinkedEntityGroup entity in linkedEntities)
                {
                    // We want to set the URPMaterialPropertyBaseColor component only on the
                    // entities that have it, so we first check.
                    if (state.EntityManager.HasComponent<URPMaterialPropertyBaseColor>(entity.Value))
                    {
                        // Set the color of each entity that makes up the tank.
                        // state.EntityManager.SetComponentData(entity.Value, color);
                        RefRW<URPMaterialPropertyBaseColor> colorRef = SystemAPI.GetComponentRW<URPMaterialPropertyBaseColor>(entity.Value);
                        colorRef.ValueRW.Value = randomColor;
                    }
                }
            }
        }

        private static float4 RandomColor(ref Random random)
        {
            // 0.618034005f is inverse of the golden ratio
            var hue = (random.NextFloat() + 0.618034005f) % 1;
            return (Vector4)Color.HSVToRGB(hue, 1.0f, 1.0f);
        }
    }
}
