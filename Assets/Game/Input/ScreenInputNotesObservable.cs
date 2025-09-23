using Beatmaps;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public sealed class ScreenInputNotesObservable : MonoBehaviour, IInputNotesObservable
	{
		public Observable<Notes> OnNotePressed => _onNotePressed;
		private Observable<Notes> _onNotePressed;

		[SerializeField]
		private Button _blueButton;
		[SerializeField]
		private Button _redButton;

		public void Awake()
		{
			_onNotePressed = Observable.Merge(
				_blueButton.OnClickAsObservable().Select(_ => Notes.Blue),
				_redButton.OnClickAsObservable().Select(_ => Notes.Red)
				);
		}
	}
}