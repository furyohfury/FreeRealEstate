using System;

namespace UIStackSystem
{
    [Serializable]
    public sealed class PagePrefabInfo
    {
        public Page PagePrefab;
        public OpenAnimationInfo DefaultOpenAnimation;
        public CloseAnimationInfo DefaultCloseAnimation;
    }
}
