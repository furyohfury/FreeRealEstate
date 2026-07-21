using UnityEngine;

namespace UIStackSystem
{
    public struct OpenPageOptions
    {
        public IPageOpenAnimation AnimationMode;
        public UILayer Layer;
        public Vector2 Position;

        public static OpenPageOptions Create()
        {
            return new OpenPageOptions();
        }
    }
}