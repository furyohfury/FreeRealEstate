using Game.Visuals;

namespace Game.BeatmapRestart
{
	public sealed class BeatmapActiveSpinnerRestartable : IBeatmapRestartable
	{
		private readonly IActiveSpinnerFactory _activeSpinnerFactory;

		public BeatmapActiveSpinnerRestartable(IActiveSpinnerFactory activeSpinnerFactory)
		{
			_activeSpinnerFactory = activeSpinnerFactory;
		}

		public void Restart()
		{
			_activeSpinnerFactory.RemoveCurrent();
		}
	}
}