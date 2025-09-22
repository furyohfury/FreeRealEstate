using R3;

namespace Game.UI
{
	public interface ILoginWindow : IWindow, IWindowClosable, IWindowBackButton
	{
		Observable<Unit> OnSignInButtonPressed { get; }
		Observable<string> Email { get; }
		Observable<string> Password { get; }

		void SetLoginButtonInteractable(bool isActive);
		void SetPasswordField(string text);
		void SetErrorField(string text);
	}
}