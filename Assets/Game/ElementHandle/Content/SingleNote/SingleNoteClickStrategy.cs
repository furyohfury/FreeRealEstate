using System;
using System.Collections.Generic;
using System.Linq;
using Beatmaps;
using Game.SongMapTime;

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

		public override ClickStatus HandleClick(MapElement element, Notes inputNote)
		{
			if (element is not SingleNote singleNote)
			{
				throw new ArgumentException("Expected single note");
			}

			return singleNote.Note == inputNote
			       && Math.Abs(element.HitTime - MapTime.GetMapTimeInSeconds()) <= _singleNoteClickIntervalParams.GetClickInterval()
				? ClickStatus.Success
				: ClickStatus.Fail;
		}

		public override void SetDifficultyParameters(IEnumerable<IDifficultyParams> parameters)
		{
			_singleNoteClickIntervalParams = parameters?.OfType<SingleNoteClickIntervalParams>().Single();
		}
	}
}