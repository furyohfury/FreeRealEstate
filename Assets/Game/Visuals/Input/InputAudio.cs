using System;
using Audio;
using Cysharp.Threading.Tasks;
using R3;
using VContainer.Unity;

namespace Game.Visuals
{
	public sealed class InputAudio : IStartable, IDisposable
	{
		private const string CLIP_ID = "inputNote";
		private readonly IInputReader _inputReader;
		private readonly AudioManager _audioManager;
		private IDisposable _disposable;

		public InputAudio(IInputReader inputReader, AudioManager audioManager)
		{
			_inputReader = inputReader;
			_audioManager = audioManager;
		}

		public void Start()
		{
			_disposable = _inputReader.OnNotePressed
			                          .Subscribe(note => PlaySound());
		}

		private void PlaySound()
		{
			_audioManager.PlaySoundOneShot(CLIP_ID, AudioOutput.UI).Forget();
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}