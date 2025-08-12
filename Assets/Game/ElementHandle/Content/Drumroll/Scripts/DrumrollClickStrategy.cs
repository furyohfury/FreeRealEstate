using System;
using System.Collections.Generic;
using System.Linq;
using Beatmaps;
using Game.BeatmapControl;
using Game.SongMapTime;
using UnityEngine;

namespace Game.ElementHandle
{
	public sealed class DrumrollClickStrategy : ElementClickStrategy
	{
		private readonly BeatmapPipeline _beatmapPipeline;
		private readonly DrumrollTickrateService _drumrollTickrateService;
		private float _clickInterval;

		private Drumroll _activeDrumroll;
		private float _previousInputTime;
		private Queue<float> _drumrollNoteTimings;

		public DrumrollClickStrategy(IMapTime mapTime, BeatmapPipeline beatmapPipeline, DrumrollTickrateService drumrollTickrateService) :
			base(mapTime)
		{
			_beatmapPipeline = beatmapPipeline;
			_drumrollTickrateService = drumrollTickrateService;
		}

		public override Type GetElementType()
		{
			return typeof(Drumroll);
		}

		public override ClickResult HandleClick(MapElement element, Notes inputNote)
		{
			if (element is not Drumroll drumroll)
			{
				throw new ArgumentException("Expected drumroll");
			}

			if (IsNewDrumroll(drumroll))
			{
				var currentMap = _beatmapPipeline.Map.CurrentValue;
				var bpm = currentMap.GetBpm();
				var drumRollNoteCount = _drumrollTickrateService.GetNotesCountByDuration(bpm, drumroll.Duration);
				var timeBetweenNotes = drumroll.Duration / drumRollNoteCount;
				_drumrollNoteTimings = new Queue<float>();
				for (int i = 0, count = drumRollNoteCount; i < count; i++)
				{
					_drumrollNoteTimings.Enqueue(drumroll.HitTime + timeBetweenNotes * i);
				}
			}

			var mapTime = MapTime.GetMapTimeInSeconds();
			float noteTiming;
			while (_drumrollNoteTimings.TryPeek(out noteTiming) && mapTime > noteTiming + _clickInterval)
			{
				_drumrollNoteTimings.Dequeue();
			}

			if (_drumrollNoteTimings.Count <= 0)
			{
				return new NoneClickResult();
			}

			if (ClickWasInsideInterval(mapTime, noteTiming))
			{
				Debug.Log("Hit drumroll note");
				_drumrollNoteTimings.Dequeue();
				return _drumrollNoteTimings.Count <= 0
					? new DrumrollCompleteClickResult()
					: new DrumrollHitClickResult();
			}

			return new NoneClickResult();
		}

		private bool IsNewDrumroll(Drumroll drumroll)
		{
			return _activeDrumroll != drumroll;
		}

		private bool ClickWasInsideInterval(float mapTime, float noteTiming)
		{
			return MathF.Abs(mapTime - noteTiming) <= _clickInterval;
		}

		public override void SetDifficultyParameters(IEnumerable<IDifficultyParams> parameters)
		{
			_clickInterval = parameters.OfType<DrumrollClickIntervalParams>().Single().GetClickInterval();
		}
	}
}