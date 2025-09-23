using Beatmaps;
using R3;

namespace Game
{
	public interface IInputNotesObservable
	{
		public Observable<Notes> OnNotePressed { get; }
	}
}