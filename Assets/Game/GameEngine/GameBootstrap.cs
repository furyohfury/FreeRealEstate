using System;
using System.Collections.Generic;
using Debugging;
using Unity.Entities;

namespace Game.GameEngine
{
    public class GameBootstrap : ICustomBootstrap
    {
        public bool Initialize(string defaultWorldName)
        {
            // 1. Создаем мир и назначаем его миром по умолчанию
            var world = new World(defaultWorldName);
            World.DefaultGameObjectInjectionWorld = world;

            // 2. Получаем вообще все системы, доступные в сборках (Unity + твои)
            var allSystemTypes = DefaultWorldInitialization.GetAllSystems(WorldSystemFilterFlags.Default);

            // 3. Отсеиваем твои системы, чтобы они не создались автоматически.
            // Здесь мы оставляем только те, что НЕ принадлежат твоему неймспейсу.
            var unitySystems = new List<Type>();
            foreach (var type in allSystemTypes)
            {
                // Замени "MyGame" на корневой неймспейс твоего проекта
                if (type.Namespace != null
                    && (type.Namespace.StartsWith("Game") || type.Namespace.StartsWith(nameof(Debugging))))
                {
                    continue;
                }
                unitySystems.Add(type);
            }

            // 4. Инициализируем стандартные системы Unity (Рендер, Физика, Трансформы)
            DefaultWorldInitialization.AddSystemsToRootLevelSystemGroups(world, unitySystems);

            // 5. РУЧНОЕ ДОБАВЛЕНИЕ ТВОИХ СИСТЕМ
            // Теперь ты сам решаешь, когда и в какую группу их засунуть.
            GameplayGroup gameplayGroup = world.GetOrCreateSystemManaged<GameplayGroup>();
            SimulationSystemGroup simGroup = world.GetExistingSystemManaged<SimulationSystemGroup>();
            simGroup.AddSystemToUpdateList(gameplayGroup);
            simGroup.SortSystems();
            AddSystemsToGameplayGroup(world, gameplayGroup);
            ScriptBehaviourUpdateOrder.AppendWorldToCurrentPlayerLoop(world);

            return true; // Возвращаем true, чтобы Unity не запускала стандартный процесс инициализации
        }

        private static void AddSystemsToGameplayGroup(World world, GameplayGroup gameplayGroup)
        {
            // var tankMoveSystem = world.CreateSystem<TankMovementAISystem>();
            // gameplayGroup.AddSystemToUpdateList(tankMoveSystem);
            //
            // SystemHandle playersystem = world.CreateSystem<PlayerSystem>();
            // gameplayGroup.AddSystemToUpdateList(playersystem);
            //
            // SystemHandle TankShootingSystem = world.CreateSystem<TankShootingSystem>();
            // gameplayGroup.AddSystemToUpdateList(TankShootingSystem);
            //
            // SystemHandle TankSpawnSystem = world.CreateSystem<TankSpawnSystem>();
            // gameplayGroup.AddSystemToUpdateList(TankSpawnSystem);
        }
    }
}
