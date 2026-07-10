namespace UIStackSystem
{
    public struct ClosePageOptions
    {
        public IPageCloseAnimation AnimationMode;

        public static ClosePageOptions Create()
        {
            return new ClosePageOptions();
        }
    }
}