using System;
using Cysharp.Threading.Tasks;
using ObjectProvide;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.UI
{
	public sealed class LoginWindowFactory
	{
		private readonly IObjectProvider _objectProvider;
		private readonly Transform _container;
		private readonly Func<ILoginWindow, LoginWindowPresenter> _factory;

		private LoginWindowPresenter _presenter;
		private const string PREFAB_ID = "LoginWindow";

		public LoginWindowFactory(
			IObjectProvider objectProvider,
			Transform container,
			Func<ILoginWindow, LoginWindowPresenter> factory
			)
		{
			_objectProvider = objectProvider;
			_container = container;
			_factory = factory;
		}

		public async UniTask Spawn()
		{
			LoginWindow prefab = await _objectProvider.Get<LoginWindow>(PREFAB_ID);
			LoginWindow view = Object.Instantiate(prefab, _container);
			_presenter = _factory.Invoke(view);
			_presenter.Initialize();
		}
	}
}