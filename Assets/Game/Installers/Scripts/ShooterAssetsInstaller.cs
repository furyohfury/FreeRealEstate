using Game.Scripts.Shooter;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(fileName = nameof(ShooterAssetsInstaller), menuName = "Game/Installers/" + nameof(ShooterAssetsInstaller))]
    public sealed class ShooterAssetsInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private Player _player;
        [SerializeField]
        private ScoreSettingsConfig _scoreSettings;
        [SerializeField]
        private NetworkPrefabsList[] _networkPrefabsList;

        public override void InstallBindings()
        {
            Container.Bind<Player>().FromInstance(_player).AsSingle();

            Container.Bind<ScoreSettingsConfig>().FromInstance(_scoreSettings).AsSingle();

            Container.Bind<NetworkPrefabsList[]>().FromInstance(_networkPrefabsList).AsCached();
        }
    }
}
