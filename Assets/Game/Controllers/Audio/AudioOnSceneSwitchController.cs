using System;
using Audio;
using Game.SceneSwitch;
using VContainer.Unity;

namespace Game.Controllers.Audio
{
	public sealed class AudioOnSceneSwitchController : IInitializable, IDisposable
	{
		private readonly AudioManager _audioManager;

		public AudioOnSceneSwitchController(AudioManager audioManager)
		{
			_audioManager = audioManager;
		}

		public void Initialize()
		{
			ISceneSwitchable.OnSceneSwitched += OnSceneSwitched;
		}

		private void OnSceneSwitched(string obj)
		{
			_audioManager.StopAudioOutput(AudioOutput.Music);
		}

		public void Dispose()
		{
			ISceneSwitchable.OnSceneSwitched -= OnSceneSwitched;
		}
	}
}