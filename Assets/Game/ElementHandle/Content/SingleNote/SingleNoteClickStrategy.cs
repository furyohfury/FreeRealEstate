using System;
using System.Collections.Generic;
using System.Linq;
using Beatmaps;
using Game.BeatmapTime;

namespace Game.ElementHandle
{
	public sealed class SingleNoteClickStrategy : ElementClickStrategy
	{
		private SingleNoteClickIntervalParams _singleNoteClickIntervalParams;

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
			       && offset <= _singleNoteClickIntervalParams.GetClickInterval()
				? new NoteHitHandleResult(element, offset)
				: new MissHandleResult(element);
		}

		public override void SetDifficultyParameters(IEnumerable<IDifficultyParams> parameters)
		{
			_singleNoteClickIntervalParams = parameters?.OfType<SingleNoteClickIntervalParams>().Single();
		}
	}
}