using UnityEngine;

namespace UIStackSystem
{
    public sealed class OpenPageOptions
    {
        public IPageOpenAnimation AnimationMode;
        public UILayer Layer;
        public Vector2 Position;
        public float Duration;

        public static OpenPageOptions Create()
        {
            return new OpenPageOptions();
        }
    }
}