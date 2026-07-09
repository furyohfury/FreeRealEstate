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

        private void OnEnable()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
        }

        private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            var tempStack = new  Stack<PageContext>();
            
            while (_stack.Count > 0)
            {
                PageContext topPageContext = _stack.Peek();

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

        public async UniTask<T> OpenPage<T>(OpenPageOptions openPageOptions) where T : IPresenter // layers?
        {
            T presenter = _presenterFactory.Create<T>();
            presenter.Init();
            Page<T> pagePrefab = _uiRegistry.GetPagePrefab<T>();
            Page<T> spawnedPage = Instantiate(pagePrefab, _container);

            if (openPageOptions.AnimationMode == UIPageAnimationMode.None)
            {
                openPageOptions.AnimationMode = _uiRegistry.GetDefaultOpenAnimationMode<T>();
            }

            _stack.Push(new PageContext
                        {
                            Page = spawnedPage
                            , Presenter = presenter
                            , ShowAnimation = openPageOptions.AnimationMode
                        });

            await spawnedPage.Open(presenter, openPageOptions);

            return presenter;
        }

        public async UniTask<T> OpenPage<T>() where T : IPresenter // TODO delete and make default param.
                                                                   // But need to remake options mb if animations will be 
                                                                   // interfaces mb options will be classes
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

            await page.Close(closePageOptions); // TODO default options

            page.DestroyPage();
        }

        public async UniTask<T> ReplaceCurrentPage<T>(OpenPageOptions openPageOptions = default, ClosePageOptions closePageOptions = default) where T : IPresenter
        {
            await CloseTop(closePageOptions);

            return await OpenPage<T>(openPageOptions);
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
