using System;
using Zenject;

namespace UIStackSystem
{
    public sealed class PresenterFactory
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

        public IPresenter Create(Type type)
        {
            var presenter = _container.Instantiate(type);

            return presenter as IPresenter;
        }
    }
}