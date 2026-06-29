using System;

namespace UIStackSystem
{
    public interface IPresenter : IDisposable
    {
        void Init();
    }
}