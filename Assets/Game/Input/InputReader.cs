using System.Collections.Generic;
using System.Linq;
using Beatmaps;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class InputReader : IInputReader, IPostInitializable
	{
		public Observable<Notes> OnNotePressed { get; private set; }
		public Observable<Unit> OnRestart { get; private set; }

		private readonly IEnumerable<IInputNotesObservable> _notesObservables;
		private readonly IInputRestartable _inputRestartable;

		public InputReader(IEnumerable<IInputNotesObservable> notesObservables, IInputRestartable inputRestartable)
		{
			_notesObservables = notesObservables;
			_inputRestartable = inputRestartable;
		}

		public void PostInitialize()
		{
			OnNotePressed = _notesObservables
			                .Select(observable => observable.OnNotePressed)
			                .Merge();

			OnRestart = _inputRestartable.OnRestart;
		}
	}
}