using Zenject;

namespace UIStackSystem
{
    public sealed class PresenterFactory : IPresenterFactory
    {
        private readonly DiContainer _container;

        public PresenterFactory(DiContainer container)
        {
            _container = container;
        }

        public T Create<T>() where T : IPresenter
        {
            var presenter = _container.Instantiate<T>();

            return presenter;
        }
    }
}