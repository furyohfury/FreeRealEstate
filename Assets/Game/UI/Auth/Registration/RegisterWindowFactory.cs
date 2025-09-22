using System;
using Cysharp.Threading.Tasks;
using ObjectProvide;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.UI
{
	public sealed class RegisterWindowFactory
	{
		private readonly IObjectProvider _objectProvider;
		private readonly Func<IRegisterWindow, RegistrationWindowPresenter> _presenterFactory;
		private readonly Transform _container;

		private RegistrationWindowPresenter _presenter;
		private const string PREFAB_ID = "RegisterWindow";

		public RegisterWindowFactory(
			IObjectProvider objectProvider,
			Transform container,
			Func<IRegisterWindow, RegistrationWindowPresenter> presenterFactory
			)
		{
			_objectProvider = objectProvider;
			_container = container;
			_presenterFactory = presenterFactory;
		}

		public async UniTask Spawn()
		{
			RegisterWindow prefab = await _objectProvider.Get<RegisterWindow>(PREFAB_ID);
			RegisterWindow view = Object.Instantiate(prefab, _container);
			_presenter = _presenterFactory.Invoke(view);
			_presenter.Initialize();
		}
	}
}