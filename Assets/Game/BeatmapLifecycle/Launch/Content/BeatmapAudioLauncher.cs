using Audio;
using Cysharp.Threading.Tasks;

namespace Game.BeatmapLaunch
{
	public sealed class BeatmapAudioLauncher : IBeatmapLaunchable
	{
		private readonly AudioManager _audioManager;

		public BeatmapAudioLauncher(AudioManager audioManager)
		{
			_audioManager = audioManager;
		}

		public async UniTask Launch(BeatmapLaunchContext context)
		{
			await _audioManager.PlaySound(context.Bundle.SongId, AudioOutput.Music, startTime: context.SelectedVariant.SongStartTimeInSeconds);
		}
	}
}