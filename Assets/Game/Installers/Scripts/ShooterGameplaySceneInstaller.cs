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

            Container.BindInterfacesTo<SpawnManager>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<AimIcon>()
                     .FromComponentInHierarchy()
                     .AsSingle();
        }
    }
}
