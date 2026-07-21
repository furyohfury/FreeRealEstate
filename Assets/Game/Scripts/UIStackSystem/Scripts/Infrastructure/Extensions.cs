using UnityEngine;

namespace UIStackSystem
{
    public static class Extensions
    {
        public static OpenPageOptions ToOptions(this OpenAnimationInfo info)
        {
            return new OpenPageOptions
                   {
                       Animation = info.Animation,
                       Duration = info.Duration
                   };
        }

        public static ClosePageOptions ToOptions(this CloseAnimationInfo info)
        {
            return new ClosePageOptions
                   {
                       Animation = info.Animation,
                       Duration = info.Duration
                   };
        }

        public static OpenPageOptions WithAnimationMode(this OpenPageOptions options, IPageOpenAnimation mode)
        {
            options.Animation = mode;

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
            options.Animation = mode;

            return options;
        }
    }
}
