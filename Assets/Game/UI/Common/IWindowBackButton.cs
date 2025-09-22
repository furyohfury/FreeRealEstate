using R3;

namespace Game.UI
{
	public interface IWindowBackButton
	{
		Observable<Unit> OnBackButtonPressed { get; }
	}
}