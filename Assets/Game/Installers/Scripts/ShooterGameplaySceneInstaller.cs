using Zenject;

namespace Game
{
    public sealed class ShooterGameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SpawnPoint>()
                     .FromComponentsInHierarchy()
                     .AsCached();

            Container.BindInterfacesAndSelfTo<PlayerSpawnSystem>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<AimIcon>()
                     .FromComponentInHierarchy()
                     .AsSingle();

            Container.BindInterfacesTo<PlayerDeathObserver>()
                     .AsSingle();

            Container.Bind<SessionSystem>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}
