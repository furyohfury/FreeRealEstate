using Game.Auth;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(fileName = nameof(ShooterProjectInstaller), menuName = "Game/Installers/" + nameof(ShooterProjectInstaller))]
    public sealed class ShooterProjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private NetworkManager _networkManagerPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AuthorizationSystem>()
                     .AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerProfile>()
                     .AsSingle();

            Container.Bind<LobbySystem>()
                     .AsSingle();

            Container.Bind<NetworkManager>()
                     .FromComponentInNewPrefab(_networkManagerPrefab)
                     .AsSingle()
                     .NonLazy();

            Container.BindInterfacesTo<ProjectEntryPoint>()
                     .AsSingle();
        }
    }
}
