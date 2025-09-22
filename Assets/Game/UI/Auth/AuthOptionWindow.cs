using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public sealed class AuthOptionWindow : MonoBehaviour, IAuthOptionWindow
	{
		public Observable<Unit> OnRegisterButtonPressed { get; private set; }
		public Observable<Unit> OnLoginButtonPressed { get; private set; }

		[SerializeField]
		private Button _loginButton;
		[SerializeField]
		private Button _registerButton;

		public void Close()
		{
			Destroy(gameObject);
		}

		private void Awake()
		{
			OnRegisterButtonPressed = _registerButton.OnClickAsObservable();
			OnLoginButtonPressed = _loginButton.OnClickAsObservable();
		}
	}
}