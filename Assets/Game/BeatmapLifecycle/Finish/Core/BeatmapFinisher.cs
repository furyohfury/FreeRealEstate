using System.Collections.Generic;

namespace Game.BeatmapFinish
{
	public sealed class BeatmapFinisher
	{
		private readonly IEnumerable<IBeatmapFinishable> _finishables;

		public BeatmapFinisher(IEnumerable<IBeatmapFinishable> finishables)
		{
			_finishables = finishables;
		}

		public void Finish()
		{
			foreach (var finishable in _finishables)
			{
				finishable.Finish();
			}
		}
	}
}