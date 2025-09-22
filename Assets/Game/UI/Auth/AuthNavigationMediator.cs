using Cysharp.Threading.Tasks;

namespace Game.UI
{
	public class AuthNavigationMediator : IAuthNavigationMediator
	{
		private readonly LoginWindowFactory _loginWindowFactory;
		private readonly RegisterWindowFactory _registerWindowFactory;
		private readonly AuthOptionWindowFactory _authOptionWindowFactory;

		public AuthNavigationMediator(
			LoginWindowFactory loginWindowFactory,
			RegisterWindowFactory registerWindowFactory,
			AuthOptionWindowFactory authOptionWindowFactory
			)
		{
			_loginWindowFactory = loginWindowFactory;
			_registerWindowFactory = registerWindowFactory;
			_authOptionWindowFactory = authOptionWindowFactory;
		}

		public async UniTask ShowOptions()
		{
			await _authOptionWindowFactory.Spawn();
		}

		public async UniTask ShowLoginWindow()
		{
			await _loginWindowFactory.Spawn();
		}

		public async UniTask ShowRegisterWindow()
		{
			await _registerWindowFactory.Spawn();
		}
	}
}