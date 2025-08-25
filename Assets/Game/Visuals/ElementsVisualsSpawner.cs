using System;
using Beatmaps;
using Game.BeatmapTime;
using Game.Services;
using R3;

namespace Game.Visuals
{
	public sealed class ElementsVisualsSpawner : IDisposable
	{
		public Observable<ElementViewPair> OnElementViewSpawned => _onElementViewSpawned;

		private readonly ElementViewFactory _elementViewFactory;
		private readonly IMapTime _mapTime;
		private readonly ElementViewContainerService _containerService;

		private readonly Subject<ElementViewPair> _onElementViewSpawned = new();
		private IBeatmap _activeBeatmap;
		private int _index = 0;
		private readonly CompositeDisposable _disposable = new();

		public ElementsVisualsSpawner(
			ElementViewFactory elementViewFactory,
			IMapTime mapTime,
			ElementViewContainerService containerService
		)
		{
			_elementViewFactory = elementViewFactory;
			_mapTime = mapTime;
			_containerService = containerService;
		}

		public void LaunchMap(IBeatmap map)
		{
			_activeBeatmap = map;
			Observable.EveryUpdate()
			          .Subscribe(_ => OnTick())
			          .AddTo(_disposable);
		}

		public void Reset()
		{
			_index = 0;
		}

		private void OnTick() // TODO fix. Spawn with position to adjust time error caused by deltatime. Actually its in the mover
		{
			SpawnNewElements();
		}

		private bool IsMapEnded(MapElement[] mapElements)
		{
			return _index >= mapElements.Length;
		}

		private void SpawnNewElements()
		{
			MapElement[] mapElements = _activeBeatmap.GetMapElements();
			if (IsMapEnded(mapElements))
			{
				StopMap();
				return;
			}

			while (IsMapEnded(mapElements) == false)
			{
				var element = mapElements[_index];

				if (!ElementTimeIsInScrollRange(element.HitTime))
				{
					break;
				}

				var view = SpawnView(element);
				var pair = new ElementViewPair(element, view);
				_onElementViewSpawned.OnNext(pair);
				_index++;
			}
		}

		private bool ElementTimeIsInScrollRange(float elementTime)
		{
			return elementTime - NotesVisualStaticData.SCROLL_TIME <= _mapTime.GetMapTimeInSeconds();
		}

		private ElementView SpawnView(MapElement element)
		{
			var view = _elementViewFactory.Spawn(element, _containerService.Container);
			return view;
		}

		private void StopMap()
		{
			_disposable.Clear();
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}