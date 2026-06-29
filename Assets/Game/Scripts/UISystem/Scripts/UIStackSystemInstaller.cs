using UnityEngine;
using Zenject;

namespace UIStackSystem
{
    [CreateAssetMenu(menuName = nameof(UIStackSystem) + "/" + nameof(UIStackSystemInstaller), fileName = nameof(UIStackSystemInstaller))]
    public sealed class UIStackSystemInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private UIManager _uiManagerPrefab;

        public override void InstallBindings()
        {
            Container.Bind<PresenterFactory>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<UIManager>()
                     .FromComponentInNewPrefab(_uiManagerPrefab)
                     .AsSingle();
        }
    }
}
