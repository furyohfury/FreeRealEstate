using R3;

namespace Game.UI
{
	public sealed class AuthOptionWindowPresenter
	{
		private readonly IAuthOptionWindow _authOptionWindow;
		private readonly IAuthNavigationMediator _authNavigationMediator;
		private readonly CompositeDisposable _disposable = new();

		public AuthOptionWindowPresenter(
			IAuthOptionWindow authOptionWindow,
			IAuthNavigationMediator authNavigationMediator
			)
		{
			_authOptionWindow = authOptionWindow;
			_authNavigationMediator = authNavigationMediator;
		}

		public void Initialize()
		{
			_authOptionWindow.OnRegisterButtonPressed
			                 .Subscribe(_ =>
			                 {
				                 _authNavigationMediator.ShowRegisterWindow();
				                 _authOptionWindow.Close();
				                 _disposable.Dispose();
			                 })
			                 .AddTo(_disposable);

			_authOptionWindow.OnLoginButtonPressed
			                 .Subscribe(_ =>
			                 {
				                 _authNavigationMediator.ShowLoginWindow();
				                 _authOptionWindow.Close();
				                 _disposable.Dispose();
			                 })
			                 .AddTo(_disposable);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}