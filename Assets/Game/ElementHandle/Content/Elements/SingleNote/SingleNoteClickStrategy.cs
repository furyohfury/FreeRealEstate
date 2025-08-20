using System;
using System.Collections.Generic;
using System.Linq;
using Beatmaps;
using Game.BeatmapTime;

namespace Game.ElementHandle
{
	public sealed class SingleNoteClickStrategy : ElementClickStrategy
	{
		private float _clickWindow;

		public SingleNoteClickStrategy(IMapTime mapTime) : base(mapTime)
		{
		}

		public override Type GetElementType()
		{
			return typeof(SingleNote);
		}

		public override HandleResult HandleClick(MapElement element, Notes inputNote)
		{
			if (element is not SingleNote singleNote)
			{
				throw new ArgumentException("Expected single note");
			}

			var offset = Math.Abs(element.HitTime - MapTime.GetMapTimeInSeconds());
			return singleNote.Note == inputNote
			       && offset <= _clickWindow
				? new NoteHitHandleResult(element, offset, _clickWindow)
				: new MissHandleResult(element);
		}

		public override void SetDifficultyParameters(IEnumerable<IDifficultyParams> parameters)
		{
			_clickWindow = parameters.OfType<SingleNoteClickIntervalParams>().Single().GetClickInterval();
		}
	}
}