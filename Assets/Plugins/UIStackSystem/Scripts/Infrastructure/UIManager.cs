using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace UIStackSystem
{
    public sealed class UIManager : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _container;

        private IPresenterFactory _presenterFactory;
        private UIRegistry _uiRegistry;
        private readonly Stack<PageContext> _stack = new Stack<PageContext>();

        [Inject]
        public void Construct(IPresenterFactory presenterFactory)
        {
            _presenterFactory = presenterFactory;
            _uiRegistry = Resources.Load<UIRegistry>(nameof(UIRegistry));
            _uiRegistry.Initialize();
        }

        private void OnEnable()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
        }

        private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            var tempStack = new Stack<PageContext>();

            while (_stack.Count > 0)
            {
                PageContext topPageContext = _stack.Pop();

                if (topPageContext.IsPersistentThroughScenes)
                {
                    tempStack.Push(topPageContext);
                    continue;
                }

                CloseTop();
            }

            while (tempStack.Count > 0)
            {
                _stack.Push(tempStack.Pop());
            }
        }

        public async UniTask<TPresenter> OpenPage<TPresenter>(OpenPageOptions openPageOptions = null) where TPresenter : IPresenter // layers?
        {
            TPresenter presenter = _presenterFactory.Create<TPresenter>();
            presenter.Init();
            Page<TPresenter> pagePrefab = _uiRegistry.GetPagePrefab<TPresenter>();
            Page<TPresenter> spawnedPage = Instantiate(pagePrefab, _container);
            openPageOptions ??= CreateOpenPageOptions<TPresenter>();

            _stack.Push(new PageContext
                        {
                            Page = spawnedPage,
                            Presenter = presenter,
                            ShowAnimation = openPageOptions.Animation
                        });

            await spawnedPage.Open(presenter, openPageOptions);

            return presenter;
        }

        public async UniTask CloseTop(ClosePageOptions closePageOptions = null)
        {
            if (_stack.Count <= 0)
            {
                return;
            }

            PageContext pageContext = _stack.Pop();
            IPresenter presenter = pageContext.Presenter;
            presenter.Dispose();
            IPage page = pageContext.Page;
            closePageOptions ??= CreateClosePageOptions(presenter.GetType());

            await page.Close(closePageOptions);

            page.DestroyPage();
        }

        public async UniTask<T> ReplaceCurrentPage<T>(OpenPageOptions openPageOptions = null, ClosePageOptions closePageOptions = null)
            where T : IPresenter
        {
            await CloseTop(closePageOptions);

            return await OpenPage<T>(openPageOptions);
        }

        public OpenPageOptions CreateOpenPageOptions<TPresenter>() where TPresenter : IPresenter
        {
            OpenAnimationInfo animationInfo = _uiRegistry.GetDefaultOpenAnimationInfo<TPresenter>();
            OpenPageOptions defaultPageOptions = animationInfo.ToOptions();

            return defaultPageOptions;
        }

        public ClosePageOptions CreateClosePageOptions(Type presenterType)
        {
            CloseAnimationInfo animationInfo = _uiRegistry.GetDefaultCloseAnimationInfo(presenterType);
            ClosePageOptions defaultPageOptions = animationInfo.ToOptions();

            return defaultPageOptions;
        }

        public ClosePageOptions CreateClosePageOptions<TPresenter>() where TPresenter : IPresenter
        {
            return CreateClosePageOptions(typeof(TPresenter));
        }

        public Vector2 GetCanvasSize()
        {
            return _container.sizeDelta;
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
        }
    }
}
