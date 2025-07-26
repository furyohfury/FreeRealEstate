using Beatmaps;
using DG.Tweening;
using Game.SongMapTime;
using R3;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class NotesVisualSystem
	{
		private readonly ElementViewFactory _elementViewFactory;
		private readonly IMapTime _mapTime;
		private readonly Transform _container;
		private readonly Transform _startPoint;
		private readonly Transform _endPoint;

		private int _index = 0;
		private const float SCROLL_TIME = 3f;
		private readonly CompositeDisposable _disposable = new();

		public NotesVisualSystem(
			ElementViewFactory elementViewFactory,
			IMapTime mapTime,
			Transform container,
			Transform startPoint,
			Transform endPoint
		)
		{
			_elementViewFactory = elementViewFactory;
			_mapTime = mapTime;
			_container = container;
			_startPoint = startPoint;
			_endPoint = endPoint;
		}

		public void LaunchMap(ISongMap map)
		{
			Observable.EveryUpdate()
			          .Subscribe(_ => OnTick(map))
			          .AddTo(_disposable);
		}

		public void OnTick(ISongMap map)
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

				if (!ElementTimeIsInScrollRange(element.TimeSeconds))
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
			return elementTime - SCROLL_TIME <= _mapTime.GetMapTimeInSeconds();
		}

		private ElementView SpawnView(MapElement element)
		{
			var view = _elementViewFactory.Spawn(element, _container);
			return view;
		}

		private void SetMovement(ElementView view)
		{
			view.Move(_startPoint.position);
			DOTween.To(
				view.GetPosition,
				view.Move,
				_endPoint.position,
				SCROLL_TIME)
			       .SetEase(Ease.Linear);
		}
	}
}