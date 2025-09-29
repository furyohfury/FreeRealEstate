using System;
using System.Threading;
using Beatmaps;
using Cysharp.Threading.Tasks;
using Game.BeatmapControl;
using Game.Services;
using ObjectProvide;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.Visuals
{
	public sealed class DrumRollViewFactory : IElementFactory, IAsyncStartable
	{
		private readonly ScreenBeatmapBoundsService _screenBeatmapBoundsService;
		private readonly DrumrollTickrateService _drumrollTickrateService;
		private readonly BeatmapPipeline _beatmapPipeline;
		private readonly IObjectProvider _objectProvider;
		private DrumrollView _prefab;

		private const string ID = "DrumrollPrefab";

		public DrumRollViewFactory(
			ScreenBeatmapBoundsService screenBeatmapBoundsService,
			DrumrollTickrateService drumrollTickrateService,
			BeatmapPipeline beatmapPipeline,
			IObjectProvider objectProvider
			)
		{
			_screenBeatmapBoundsService = screenBeatmapBoundsService;
			_drumrollTickrateService = drumrollTickrateService;
			_beatmapPipeline = beatmapPipeline;
			_objectProvider = objectProvider;
		}

		public async UniTask StartAsync(CancellationToken cancellation = new())
		{
			_prefab = await _objectProvider.Get<DrumrollView>(ID);
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
			var scrollDistance = _screenBeatmapBoundsService.GetNoteLineDistance();
			var length = drumroll.Duration / NotesStaticData.SCROLL_TIME * scrollDistance;

			var view = Object.Instantiate(_prefab, parent);
			view.SetLength(length);
			view.SetScorePoints(drumrollNotesCount);
			return view;
		}
	}
}