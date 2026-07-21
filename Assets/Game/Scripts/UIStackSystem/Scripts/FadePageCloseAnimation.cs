using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UIStackSystem
{
    public class FadePageCloseAnimation : IPageCloseAnimation
    {
        public UniTask PlayAnimation(PageAnimationContext context)
        {
            var canvasGroup = context.RectTransform.gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 1;

            return canvasGroup.DOFade(0, context.Duration)
                              .OnComplete(() => Object.Destroy(canvasGroup)).ToUniTask();
        }
    }
}