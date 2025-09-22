using R3;

namespace Game.UI
{
	public interface IRegisterWindow : IWindow, IWindowClosable, IWindowBackButton
	{
		Observable<Unit> OnRegisterButtonPressed { get; }
		Observable<string> Email { get; }
		Observable<string> Password { get; }
		Observable<string> Nickname { get; }

		void SetRegisterButtonInteractable(bool isActive);
		void SetPasswordField(string text);
		void SetErrorField(string text);
	}
}