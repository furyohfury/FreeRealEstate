using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Meta.Authentication;
using Game.SceneSwitch;
using R3;
using UnityEngine;

namespace Game.UI
{
	public sealed class RegistrationWindowPresenter
	{
		private readonly IRegisterWindow _registerWindow;
		private readonly IRegisterable _registerable;
		private readonly IAuthNavigationMediator _authNavigationMediator;
		private readonly ISceneSwitchable _sceneSwitchable;
		private readonly CompositeDisposable _disposable = new();

		public RegistrationWindowPresenter(
			IRegisterWindow registerWindow,
			IRegisterable registerable,
			IAuthNavigationMediator authNavigationMediator,
			ISceneSwitchable sceneSwitchable
			)
		{
			_registerWindow = registerWindow;
			_registerable = registerable;
			_authNavigationMediator = authNavigationMediator;
			_sceneSwitchable = sceneSwitchable;
		}

		public void Initialize()
		{
			var fieldsStream = Observable.CombineLatest(
				_registerWindow.Email.Merge(Observable.Return("")),
				_registerWindow.Password.Merge(Observable.Return("")),
				_registerWindow.Nickname.Merge(Observable.Return(""))
				);

			fieldsStream
				.Select(tuple => CanBeRegistered(tuple[0], tuple[1], tuple[2]))
				.Subscribe(MakeButtonInteractable)
				.AddTo(_disposable);

			_registerWindow.OnRegisterButtonPressed
			               .WithLatestFrom(fieldsStream, (_, fields) => fields)
			               .Subscribe(OnRegister)
			               .AddTo(_disposable);

			_registerWindow.OnBackButtonPressed
			               .Subscribe(_ =>
			               {
				               DestroyRegisterWindow();
				               _authNavigationMediator.ShowOptions();
			               })
			               .AddTo(_disposable);
		}

		private async void OnRegister(string[] fields)
		{
			await Register(fields[0], fields[1], fields[2]);
		}

		private async UniTask Register(string email, string password, string nickname)
		{
			var cts = new CancellationTokenSource(5000);
			_registerWindow.SetRegisterButtonInteractable(false);
			IAuthResult result = await _registerable.Register(email, password, nickname, cts.Token);
			_registerWindow.SetRegisterButtonInteractable(true);
			if (result is SuccessAuthResult)
			{
				Debug.Log("Register!");
				DestroyRegisterWindow();
				_sceneSwitchable.SwitchScene();
			}
			else if (result is ErrorAuthResult errorResult)
			{
				_registerWindow.SetErrorField(errorResult.Error);
			}
		}

		private void MakeButtonInteractable(bool value)
		{
			_registerWindow.SetRegisterButtonInteractable(value);
		}

		private bool CanBeRegistered(string email, string password, string nickname)
		{
			return FieldsAreValid(email, password, nickname) && EmailIsValid(email);
		}

		private bool EmailIsValid(string email)
		{
			return email.Contains('@');
		}

		private bool FieldsAreValid(string email, string password, string nickname)
		{
			return string.IsNullOrEmpty(email) == false
			       && string.IsNullOrEmpty(password) == false
			       && string.IsNullOrEmpty(nickname) == false;
		}

		private void DestroyRegisterWindow()
		{
			_registerWindow.Close();
			_disposable.Dispose();
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}