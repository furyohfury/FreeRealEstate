using Cysharp.Threading.Tasks;

namespace UIStackSystem
{
    public interface IPage
    {
        UniTask Open(IPresenter presenter, OpenPageOptions options);
        UniTask Close(ClosePageOptions options);
        void DestroyPage();
    }
}