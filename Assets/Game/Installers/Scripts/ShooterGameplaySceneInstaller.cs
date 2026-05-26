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

            Container.BindInterfacesAndSelfTo<SpawnManager>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<AimIcon>()
                     .FromComponentInHierarchy()
                     .AsSingle();

            Container.BindInterfacesTo<PlayerDeathObserver>()
                     .AsSingle();
        }
    }
}
