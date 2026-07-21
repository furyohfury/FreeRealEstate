namespace UIStackSystem
{
    public interface IPresenterFactory
    {
        T Create<T>() where T : IPresenter;
    }
}
