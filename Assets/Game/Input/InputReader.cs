using System.Collections.Generic;
using System.Linq;
using Beatmaps;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class InputReader : IInputReader, IPostInitializable
	{
		public Observable<Notes> OnNotePressed => _onNotePressed;
		private Observable<Notes> _onNotePressed;

		private readonly IEnumerable<IInputNotesObservable> _notesObservables;

		public InputReader(IEnumerable<IInputNotesObservable> notesObservables)
		{
			_notesObservables = notesObservables;
		}

		public void PostInitialize()
		{
			_onNotePressed = _notesObservables
			                 .Select(observable => observable.OnNotePressed)
			                 .Merge();
		}
	}
}