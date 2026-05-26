using Game.Scripts.Shooter;
using UnityEngine;
using Zenject;

namespace Game
{
    [CreateAssetMenu(fileName = nameof(ShooterAssetsInstaller), menuName = "Game/Shooter/" + nameof(ShooterAssetsInstaller))]
    public sealed class ShooterAssetsInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private Player _player;

        public override void InstallBindings()
        {
            Container.Bind<Player>()
                     .FromInstance(_player)
                     .AsSingle();
        }
    }
}
