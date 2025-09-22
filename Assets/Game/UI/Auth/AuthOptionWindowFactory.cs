using System;
using Cysharp.Threading.Tasks;
using ObjectProvide;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.UI
{
	public sealed class AuthOptionWindowFactory
	{
		private readonly IObjectProvider _objectProvider;
		private readonly Func<IAuthOptionWindow, AuthOptionWindowPresenter> _presenterFactory;
		private readonly Transform _container;

		private AuthOptionWindowPresenter _windowPresenter;
		private const string PREFAB_ID = "AuthOptionWindow";

		public AuthOptionWindowFactory(
			IObjectProvider objectProvider,
			Transform container,
			Func<IAuthOptionWindow, AuthOptionWindowPresenter> presenterFactory
			)
		{
			_objectProvider = objectProvider;
			_container = container;
			_presenterFactory = presenterFactory;
		}

		public async UniTask Spawn()
		{
			var prefab = await _objectProvider.Get<AuthOptionWindow>(PREFAB_ID);
			var view = Object.Instantiate(prefab, _container);
			_windowPresenter = _presenterFactory.Invoke(view);
			_windowPresenter.Initialize();
		}
	}
}