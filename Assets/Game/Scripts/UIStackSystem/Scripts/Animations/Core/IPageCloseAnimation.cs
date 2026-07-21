using Cysharp.Threading.Tasks;

namespace UIStackSystem
{
    public interface IPageCloseAnimation
    {
        UniTask PlayAnimation(PageAnimationContext context);
    }
}