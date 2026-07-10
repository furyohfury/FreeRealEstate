using System;

namespace UIStackSystem
{
    [Serializable]
    public sealed class CloseAnimationInfo
    {
        public UIPageCloseAnimation Animation;
        public float Duration = 0.25f;
    }
}