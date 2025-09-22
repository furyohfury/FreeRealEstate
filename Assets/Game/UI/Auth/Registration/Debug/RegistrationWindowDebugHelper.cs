using Firebase.Auth;
using Game.Meta.Authentication;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Game.UI
{
	public sealed class RegistrationWindowDebugHelper : MonoBehaviour
	{
		[Inject]
		private IRegisterable _registerable;
		[Inject]
		private ILoginable _loginable;
		[SerializeField]
		private RegisterWindow _window;
		[SerializeField]
		private LoginWindow _loginWindow;
		private RegistrationWindowPresenter _registrationWindowPresenter;
		private LoginWindowPresenter _loginWindowPresenter;

		private void Start()
		{
			// _registrationWindowPresenter = new RegistrationWindowPresenter(_window, _registerable);
			// _registrationWindowPresenter.Initialize();
			// var loginWindowPresenter = new LoginWindowPresenter(_loginWindow, _loginable);
			// loginWindowPresenter.Initialize();
		}

		[Button]
		public void DebugCurrentUser()
		{
			var currentUser = FirebaseAuth.DefaultInstance.CurrentUser;
			Debug.Log(currentUser);
		}
	}
}