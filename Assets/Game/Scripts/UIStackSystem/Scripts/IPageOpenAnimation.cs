using Cysharp.Threading.Tasks;

namespace UIStackSystem
{
    public interface IPageOpenAnimation
    {
        UniTask PlayAnimation(PageAnimationContext context);
    }
}