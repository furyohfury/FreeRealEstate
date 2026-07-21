namespace UIStackSystem
{
    public sealed class PageContext
    {
        public IPage Page;
        public IPresenter Presenter;
        public IPageOpenAnimation ShowAnimation;
        public IPageCloseAnimation HideAnimation;
        public bool IsModal;
        public bool IsPersistentThroughScenes;
        public UILayer Layer;
    }
}