using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UIStackSystem
{
    public static class AnimationModeTool
    {
        private const Ease DefaultCloseEase = Ease.InCubic;
        private const Ease DefaultOpenEase = Ease.OutCubic;
        private const float SlideDuration = 0.25f;
        private const float FadeDuration = 0.15f;

        public static async UniTask PlaySlideOpenAnimation(
            OpenPageOptions options,
            RectTransform rectTransform)
        {
            RectTransform parent = rectTransform.parent as RectTransform;

            Vector2 targetPos = options.Position;
            Vector2 offset = GetOffset(options.AnimationMode, parent);

            rectTransform.anchoredPosition = targetPos + offset;

            await rectTransform
                  .DOAnchorPos(targetPos, SlideDuration)
                  .SetEase(DefaultOpenEase);
        }

        public static async UniTask PlaySlideCloseAnimation(
            ClosePageOptions options,
            RectTransform rectTransform)
        {
            RectTransform parent = rectTransform.parent as RectTransform;

            Vector2 offset = GetOffset(options.AnimationMode, parent);

            Vector2 targetPos = rectTransform.anchoredPosition + offset;

            await rectTransform
                  .DOAnchorPos(targetPos, SlideDuration)
                  .SetEase(DefaultCloseEase);
        }

        private static Vector2 GetOffset(UIPageAnimationMode mode, RectTransform parent)
        {
            if (parent == null)
                return Vector2.zero;

            Vector2 size = parent.rect.size;

            return mode switch
            {
                UIPageAnimationMode.SlideRight => new Vector2(size.x, 0),
                UIPageAnimationMode.SlideLeft  => new Vector2(-size.x, 0),
                UIPageAnimationMode.SlideUp    => new Vector2(0, size.y),
                UIPageAnimationMode.SlideDown  => new Vector2(0, -size.y),
                _ => Vector2.zero
            };
        }
    }
}