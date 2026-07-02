namespace UIStackSystem
{
    public sealed class PageContext
    {
        public IPage Page;
        public IPresenter Presenter;
        public UIPageAnimationMode ShowAnimation;
        public UIPageAnimationMode HideAnimation;
        public bool IsModal;
        public bool IsPersistentThroughScenes;
        public UILayer Layer;
    }
}