using Game.Auth;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(fileName = nameof(ShooterProjectInstaller), menuName = "Game/Installers/" + nameof(ShooterProjectInstaller))]
    public sealed class ShooterProjectInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AuthorizationSystem>().AsSingle();

            Container.BindInterfacesTo<EntryPoint>();
        }
    }
}
