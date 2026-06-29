using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(fileName = nameof(MainMenuSceneInstaller), menuName = "Game/Installers/" + nameof(MainMenuSceneInstaller))]
    public sealed class MainMenuSceneInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MainSceneEntryPoint>()
                     .AsSingle();
        }
    }
}
