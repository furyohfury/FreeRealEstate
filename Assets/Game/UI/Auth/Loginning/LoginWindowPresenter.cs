using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Meta.Authentication;
using Game.SceneSwitch;
using R3;
using UnityEngine;

namespace Game.UI
{
	public sealed class LoginWindowPresenter
	{
		private readonly ILoginWindow _loginWindow;
		private readonly ILoginable _loginable;
		private readonly IAuthNavigationMediator _authNavigationMediator;
		private readonly ISceneSwitchable _sceneSwitchable;
		private readonly CompositeDisposable _disposable = new();

		public LoginWindowPresenter(
			ILoginWindow loginWindow,
			ILoginable loginable,
			IAuthNavigationMediator authNavigationMediator,
			ISceneSwitchable sceneSwitchable
			)
		{
			_loginWindow = loginWindow;
			_loginable = loginable;
			_authNavigationMediator = authNavigationMediator;
			_sceneSwitchable = sceneSwitchable;
		}

		public void Initialize()
		{
			var fieldsStream = Observable.CombineLatest(
				_loginWindow.Email.Merge(Observable.Return("")),
				_loginWindow.Password.Merge(Observable.Return(""))
				);

			fieldsStream
				.Select(tuple => CanBeRegistered(tuple[0], tuple[1]))
				.Subscribe(MakeButtonInteractable)
				.AddTo(_disposable);

			_loginWindow.OnSignInButtonPressed
			            .WithLatestFrom(fieldsStream, (_, fields) => fields)
			            .Subscribe(OnLogin)
			            .AddTo(_disposable);

			_loginWindow.OnBackButtonPressed
			            .Subscribe(_ =>
			            {
				            DestroyLoginWindow();
				            _authNavigationMediator.ShowOptions();
			            })
			            .AddTo(_disposable);
		}

		private async void OnLogin(string[] fields)
		{
			await Login(fields[0], fields[1]);
		}

		private async UniTask Login(string email, string password)
		{
			var cts = new CancellationTokenSource(5000);
			_loginWindow.SetLoginButtonInteractable(false);
			IAuthResult result = await _loginable.Login(email, password, cts.Token);
			_loginWindow.SetLoginButtonInteractable(true);
			if (result is SuccessAuthResult)
			{
				Debug.Log("Signed in!");
				DestroyLoginWindow();
				_sceneSwitchable.SwitchScene();
			}
			else if (result is ErrorAuthResult errorResult)
			{
				_loginWindow.SetErrorField(errorResult.Error);
			}
		}

		private void MakeButtonInteractable(bool value)
		{
			_loginWindow.SetLoginButtonInteractable(value);
		}

		private bool CanBeRegistered(string email, string password)
		{
			return FieldsAreValid(email, password) && EmailIsValid(email);
		}

		private bool EmailIsValid(string email)
		{
			return email.Contains('@');
		}

		private bool FieldsAreValid(string email, string password)
		{
			return string.IsNullOrEmpty(email) == false
			       && string.IsNullOrEmpty(password) == false;
		}

		private void DestroyLoginWindow()
		{
			_loginWindow.Close();
			_disposable.Dispose();
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}