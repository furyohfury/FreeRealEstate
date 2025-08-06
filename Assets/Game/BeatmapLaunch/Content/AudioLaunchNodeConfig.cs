using Audio;
using Cysharp.Threading.Tasks;
using PriorityTaskPipeline;
using VContainer;

namespace Game.BeatmapLaunch
{
	public class AudioLaunchNodeConfig : PriorityPipelineNodeConfig
	{
		private BeatmapLaunchContext _launchContext;
		private AudioManager _audioManager;

		[Inject]
		private void Construct(AudioManager audioManager)
		{
			_audioManager = audioManager;
		}

		public override UniTask[] GetTasks()
		{
			// return new[] { _audioManager.PlaySound() };
			return null;
		}
	}
}