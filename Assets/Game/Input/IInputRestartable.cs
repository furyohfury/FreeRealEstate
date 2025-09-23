using R3;

namespace Game
{
	public interface IInputRestartable
	{
		Observable<Unit> OnRestart { get; }
	}
}