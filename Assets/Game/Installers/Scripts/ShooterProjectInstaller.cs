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
                     .FromMethod(ctx =>
                     {
                         var go = Instantiate(_networkManagerPrefab);
                         DontDestroyOnLoad(go);

                         return go.GetComponent<NetworkManager>();
                     })
                     .AsSingle()
                     .NonLazy();

            Container.BindInterfacesTo<ProjectEntryPoint>()
                     .AsSingle();
        }
    }
}
