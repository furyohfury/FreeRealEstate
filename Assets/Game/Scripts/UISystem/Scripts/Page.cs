using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UIStackSystem
{
    public class Page : MonoBehaviour, IPage
    {
        [SerializeField]
        private RectTransform _rectTransform;

        public async virtual UniTask Open(IPresenter presenter, OpenPageOptions options)
        {
            await PlayOpenAnimation(options);
        }

        public async virtual UniTask Close(ClosePageOptions options)
        {
            await PlayCloseAnimation(options);
        }

        public virtual void DestroyPage()
        {
            Destroy(gameObject);
        }

        protected async virtual UniTask PlayOpenAnimation(OpenPageOptions options)
        {
            var animationMode = options.AnimationMode;

            switch (animationMode)
            {
                case UIPageAnimationMode.NoAnimation:
                    _rectTransform.anchoredPosition = options.Position;
                    break;
                case UIPageAnimationMode.Fade:
                    break;
                case UIPageAnimationMode.SlideRight:
                case UIPageAnimationMode.SlideLeft:
                case UIPageAnimationMode.SlideUp:
                case UIPageAnimationMode.SlideDown:
                    await AnimationModeTool.PlaySlideOpenAnimation(options, _rectTransform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(animationMode), animationMode, null);
            }
        }

        protected async virtual UniTask PlayCloseAnimation(ClosePageOptions options)
        {
            var animationMode = options.AnimationMode;

            switch (animationMode)
            {
                case UIPageAnimationMode.None:
                case UIPageAnimationMode.NoAnimation:
                case UIPageAnimationMode.Fade:
                    break;
                case UIPageAnimationMode.SlideRight:
                case UIPageAnimationMode.SlideLeft:
                case UIPageAnimationMode.SlideUp:
                case UIPageAnimationMode.SlideDown:
                    await AnimationModeTool.PlaySlideCloseAnimation(options, _rectTransform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(animationMode), animationMode, null);
            }
        }
    }

    public abstract class Page<TPresenter> : Page
        where TPresenter : IPresenter
    {
    }
}
