using Game.Scripts.Shooter;
using Unity.Cinemachine;
using Zenject;

namespace Game.Installers
{
    public sealed class ShooterGameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CinemachineCamera>().FromComponentInHierarchy().AsSingle();

            Container.Bind<SpawnPoint>().FromComponentsInHierarchy().AsCached();

            Container.BindInterfacesAndSelfTo<ZenjectNetworkObjectSpawner>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle().NonLazy();

            Container.Bind<AimIcon>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesTo<PlayerDeathObserver>().AsSingle();

            Container.BindInterfacesAndSelfTo<SessionSystem>().FromComponentInHierarchy().AsSingle();

            Container.Bind<DealDamageSystem>().AsSingle();

            Container.BindInterfacesAndSelfTo<ScoreSystem>().FromComponentsInHierarchy().AsSingle();

            Container.Bind<PlayerScoreItemPresenterFactory>().AsSingle();

            Container.Bind<PlayersProvider>()
                     .AsSingle();

            Container.BindInterfacesTo<PlayersProviderController>()
                     .AsSingle();

            Container.BindInterfacesTo<PlayersHpRotationController>()
                     .AsSingle();

            BindUI();
        }

        private void BindUI()
        {
            Container.Bind<PlayerScoreTable>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerScoreTablePresenter>().AsSingle();
        }
    }
}
