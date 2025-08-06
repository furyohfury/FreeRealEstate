using Cysharp.Threading.Tasks;
using Game.Visuals;

namespace Game.BeatmapLaunch
{
	public sealed class BeatmapElementsVisualLauncher : IBeatmapLaunchable
	{
		private readonly NotesVisualSystem _notesVisualSystem;

		public BeatmapElementsVisualLauncher(NotesVisualSystem notesVisualSystem)
		{
			_notesVisualSystem = notesVisualSystem;
		}

		public async UniTask Launch(BeatmapLaunchContext context)
		{
			_notesVisualSystem.LaunchMap(context.SelectedVariant.BeatmapConfig.Beatmap);
		}
	}
}