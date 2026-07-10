using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UIStackSystem
{
    public class FadePageOpenAnimation : IPageOpenAnimation
    {
        public UniTask PlayAnimation(PageAnimationContext context)
        {
            RectTransform rectTransform = context.RectTransform;
            rectTransform.position = context.Position;
            var canvasGroup = rectTransform.gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0;

            return canvasGroup.DOFade(1, context.Duration).ToUniTask();
        }
    }
}