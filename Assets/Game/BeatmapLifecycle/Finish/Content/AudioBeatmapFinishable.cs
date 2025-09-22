using Audio;

namespace Game.BeatmapFinish
{
	public sealed class AudioBeatmapFinishable : IBeatmapFinishable
	{
		private readonly AudioManager _audioManager;

		public AudioBeatmapFinishable(AudioManager audioManager)
		{
			_audioManager = audioManager;
		}

		public void Finish()
		{
			_audioManager.StopAudioOutput(AudioOutput.Music);
		}
	}
}