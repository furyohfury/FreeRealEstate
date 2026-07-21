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
            await options.Animation.PlayAnimation(new PageAnimationContext()
                                                      {
                                                          Duration = options.Duration
                                                      });
        }

        protected async virtual UniTask PlayCloseAnimation(ClosePageOptions options)
        {
            await options.Animation.PlayAnimation(new PageAnimationContext()
                                                      {
                                                          Duration = options.Duration
                                                      });
        }
    }

    public abstract class Page<TPresenter> : Page
        where TPresenter : IPresenter
    {
    }
}
