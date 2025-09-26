using R3;

namespace Game.UI
{
	public interface IButtonView
	{
		Observable<Unit> OnButtonPressed { get; }
	}
}