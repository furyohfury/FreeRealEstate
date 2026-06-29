namespace UIStackSystem
{
    public struct ClosePageOptions
    {
        public UIPageAnimationMode AnimationMode;

        public static ClosePageOptions Create()
        {
            return new ClosePageOptions();
        }
    }
}