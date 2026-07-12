using UnityEngine;

namespace UIStackSystem
{
    public sealed class ClosePageOptions
    {
        public IPageCloseAnimation AnimationMode;
        public Vector2 Position;
        public float Duration;

        public static ClosePageOptions Create()
        {
            return new ClosePageOptions();
        }
    }
}
