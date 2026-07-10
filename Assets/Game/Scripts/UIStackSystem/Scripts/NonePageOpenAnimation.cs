using Cysharp.Threading.Tasks;

namespace UIStackSystem
{
    public class NonePageOpenAnimation : IPageOpenAnimation
    {
        public UniTask PlayAnimation(PageAnimationContext context)
        {
            context.RectTransform.position = context.Position;

            return UniTask.CompletedTask;
        }
    }
}