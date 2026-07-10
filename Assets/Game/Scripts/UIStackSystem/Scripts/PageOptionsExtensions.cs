using UnityEngine;

namespace UIStackSystem
{
    public static class PageOptionsExtensions
    {
        public static OpenPageOptions WithAnimationMode(this OpenPageOptions options, IPageOpenAnimation mode)
        {
            options.AnimationMode = mode;

            return options;
        }

        public static OpenPageOptions WithLayer(this OpenPageOptions options, UILayer layer)
        {
            options.Layer = layer;

            return options;
        }

        public static OpenPageOptions WithPosition(this OpenPageOptions options, Vector2 position)
        {
            options.Position = position;

            return options;
        }

        public static ClosePageOptions WithAnimationMode(this ClosePageOptions options, IPageCloseAnimation mode)
        {
            options.AnimationMode = mode;

            return options;
        }
    }
}