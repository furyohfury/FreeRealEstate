using Cysharp.Threading.Tasks;

namespace UIStackSystem
{
    public class PageCloseNoAnimation : IPageCloseAnimation
    {
        public UniTask PlayAnimation(PageAnimationContext context)
        {
            return UniTask.CompletedTask;
        }
    }
}
