using System;
using Beatmaps;
using Game.BeatmapControl;
using Game.ElementHandle;
using Game.Services;
using ObjectProvide;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.Visuals
{
	public sealed class DrumRollViewFactory : IElementFactory, IStartable
	{
		private readonly DrumrollPrefabConfig _drumrollPrefabConfig;
		private readonly NotesLineBoundsService _notesLineBoundsService;
		private readonly DrumrollTickrateService _drumrollTickrateService;
		private readonly BeatmapPipeline _beatmapPipeline;
		private readonly IObjectProvider _objectProvider;
		private DrumrollView _prefab;

		public DrumRollViewFactory(
			NotesLineBoundsService notesLineBoundsService,
			DrumrollTickrateService drumrollTickrateService,
			BeatmapPipeline beatmapPipeline, DrumrollPrefabConfig drumrollPrefabConfig, IObjectProvider objectProvider)
		{
			_notesLineBoundsService = notesLineBoundsService;
			_drumrollTickrateService = drumrollTickrateService;
			_beatmapPipeline = beatmapPipeline;
			_drumrollPrefabConfig = drumrollPrefabConfig;
			_objectProvider = objectProvider;
		}

		public async void Start()
		{
			_prefab = await _objectProvider.Get<DrumrollView>(_drumrollPrefabConfig.DrumrollPrefabId);
		}

		public Type GetElementType()
		{
			return typeof(Drumroll);
		}

		public ElementView Spawn(MapElement element, Transform parent)
		{
			if (element is not Drumroll drumroll)
			{
				throw new ArgumentException("Expected drumroll");
			}

			var map = _beatmapPipeline.Map.CurrentValue;
			var bpm = map.GetBpm();
			var drumrollNotesCount = _drumrollTickrateService.GetNotesCountByDuration(bpm, drumroll.Duration);
			var scrollDistance = _notesLineBoundsService.GetNoteLineDistance();
			var length = drumroll.Duration / NotesVisualData.SCROLL_TIME * scrollDistance;

			var view = Object.Instantiate(_prefab, parent);
			view.SetLength(length);
			view.SetScorePoints(drumrollNotesCount);
			return view;
		}
	}
}