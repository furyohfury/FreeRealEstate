using Cysharp.Threading.Tasks;

namespace UIStackSystem
{
    public class PageOpenNoAnimation : IPageOpenAnimation
    {
        public UniTask PlayAnimation(PageAnimationContext context)
        {
            context.RectTransform.position = context.Position;

            return UniTask.CompletedTask;
        }
    }
}