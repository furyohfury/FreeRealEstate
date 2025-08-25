using Game.ElementHandle;

namespace Game.BeatmapRestart
{
	public sealed class BeatmapSpinnerClickRestartable : IBeatmapRestartable
	{
		private readonly SpinnerClickStrategy _spinnerClickStrategy;

		public BeatmapSpinnerClickRestartable(SpinnerClickStrategy spinnerClickStrategy)
		{
			_spinnerClickStrategy = spinnerClickStrategy;
		}

		public void Restart()
		{
			_spinnerClickStrategy.Reset();
		}
	}
}