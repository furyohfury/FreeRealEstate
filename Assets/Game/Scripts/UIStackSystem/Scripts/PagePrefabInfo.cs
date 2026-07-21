using System;

namespace UIStackSystem
{
    [Serializable]
    public sealed class PagePrefabInfo
    {
        public Page PagePrefab;
        public OpenAnimationInfo DefaultOpenAnimation = new OpenAnimationInfo();
        public CloseAnimationInfo DefaultCloseAnimation = new CloseAnimationInfo();
    }
}
