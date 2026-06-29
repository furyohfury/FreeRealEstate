using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace UIStackSystem
{
    public sealed class UIManager : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _container;

        private PresenterFactory _presenterFactory;
        private UIRegistry _uiRegistry;
        private readonly Stack<PageContext> _stack = new Stack<PageContext>();

        [Inject]
        public void Construct(PresenterFactory presenterFactory)
        {
            _presenterFactory = presenterFactory;
            _uiRegistry = Resources.Load<UIRegistry>(nameof(UIRegistry));
            _uiRegistry.Initialize();
        }

        public async UniTask<T> OpenPage<T>(OpenPageOptions openPageOptions) where T : IPresenter // layers?
        {
            T presenter = _presenterFactory.Create<T>();
            presenter.Init();
            Page<T> pagePrefab = _uiRegistry.GetPagePrefab<T>();
            Page<T> spawnedPage = Instantiate(pagePrefab, _container);
            _stack.Push(new PageContext
                        {
                            Page = spawnedPage
                            , Presenter = presenter
                            , ShowAnimation = openPageOptions.AnimationMode
                        });
            
            await spawnedPage.Open(presenter, openPageOptions);

            return presenter;
        }

        public async UniTask<T> OpenPage<T>() where T : IPresenter // layers?
        {
            UIPageAnimationMode defaultOpenAnimationMode = _uiRegistry.GetDefaultOpenAnimationMode<T>();
            OpenPageOptions openPageOptions = OpenPageOptions.Create()
                                                             .WithAnimationMode(defaultOpenAnimationMode);

            return await OpenPage<T>(openPageOptions);
        }

        public async UniTask CloseTop(ClosePageOptions closePageOptions = default)
        {
            if (_stack.Count <= 0)
            {
                return;
            }

            PageContext pageContext = _stack.Pop();
            pageContext.Presenter.Dispose();
            IPage page = pageContext.Page;

            await page.Close(closePageOptions);

            page.DestroyPage();
        }
    }
}
