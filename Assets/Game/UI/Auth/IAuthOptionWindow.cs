using R3;

namespace Game.UI
{
	public interface IAuthOptionWindow : IWindow, IWindowClosable
	{
		Observable<Unit> OnRegisterButtonPressed { get; }
		Observable<Unit> OnLoginButtonPressed { get; }
	}
}