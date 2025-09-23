using Beatmaps;
using R3;

namespace Game
{
	public interface IInputReader
	{
		public Observable<Notes> OnNotePressed { get; }
		public Observable<Unit> OnRestart { get; }
	}
}