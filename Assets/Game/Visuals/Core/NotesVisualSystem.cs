using Beatmaps;
using Game.Services;
using Game.SongMapTime;
using R3;

namespace Game.Visuals
{
	public sealed class NotesVisualSystem
	{
		private readonly ElementViewFactory _elementViewFactory;
		private readonly IMapTime _mapTime;
		private readonly NotesLineBoundsService _boundsService;
		private readonly NotesLineMover _notesLineMover;

		private int _index = 0;
		private readonly CompositeDisposable _disposable = new();

		public NotesVisualSystem(
			ElementViewFactory elementViewFactory,
			IMapTime mapTime,
			NotesLineBoundsService boundsService,
			NotesLineMover notesLineMover
		)
		{
			_elementViewFactory = elementViewFactory;
			_mapTime = mapTime;
			_boundsService = boundsService;
			_notesLineMover = notesLineMover;
		}

		public void LaunchMap(IBeatmap map)
		{
			Observable.EveryUpdate()
			          .Subscribe(_ => OnTick(map))
			          .AddTo(_disposable);
		}

		public void OnTick(IBeatmap map)
		{
			MapElement[] mapElements = map.GetMapElements();
			if (IsMapEnded(mapElements))
			{
				_disposable.Clear();
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
				SetMovement(view);
				_index++;
			}
		}

		private bool IsMapEnded(MapElement[] mapElements)
		{
			return _index >= mapElements.Length;
		}

		private bool ElementTimeIsInScrollRange(float elementTime)
		{
			return elementTime - NotesVisualData.SCROLL_TIME <= _mapTime.GetMapTimeInSeconds();
		}

		private ElementView SpawnView(MapElement element)
		{
			var view = _elementViewFactory.Spawn(element, _boundsService.NotesContainer);
			return view;
		}

		private void SetMovement(ElementView view)
		{
			view.Move(_boundsService.StartPoint);
			_notesLineMover.AddElement(view);
		}
	}
}