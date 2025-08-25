using Audio;

namespace Game.BeatmapRestart
{
	public sealed class BeatmapAudioRestartable : IBeatmapRestartable
	{
		private readonly AudioManager _audioManager;

		public BeatmapAudioRestartable(AudioManager audioManager)
		{
			_audioManager = audioManager;
		}

		public void Restart()
		{
			_audioManager.StopAudioOutput(AudioOutput.Music);
		}
	}
}