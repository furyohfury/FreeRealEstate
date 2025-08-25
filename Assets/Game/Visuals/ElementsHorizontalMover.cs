using System.Collections.Generic;
using Beatmaps;
using Game.Services;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Game.Visuals
{
	public sealed class ElementsHorizontalMover : IInitializable
	{
		private readonly ScreenBeatmapBoundsService _boundsService;

		private float _speed;
		private readonly Dictionary<MapElement, ElementView> _elementViews = new();
		private readonly CompositeDisposable _disposable = new();

		public ElementsHorizontalMover(ScreenBeatmapBoundsService boundsService)
		{
			_boundsService = boundsService;
		}

		public void Initialize()
		{
			_speed = CalculateSpeed();
			Observable.EveryUpdate()
			          .Subscribe(_ => MoveViewsToLeft())
			          .AddTo(_disposable);
		}

		public void Add(MapElement element, ElementView view)
		{
			if (_elementViews.TryAdd(element, view))
			{
				MoveToStartPoint(view);
			}
			else
			{
				Debug.LogWarning($"Mover already has element {element}");
			}
		}

		public void Remove(MapElement element)
		{
			_elementViews.Remove(element);
		}

		private float CalculateSpeed()
		{
			var distance = Vector2.Distance(_boundsService.HitPoint, _boundsService.StartPoint);
			var speed = distance / NotesVisualStaticData.SCROLL_TIME;
			return speed;
		}

		private void MoveToStartPoint(ElementView view)
		{
			view.Move(_boundsService.StartPoint);
		}

		private void MoveViewsToLeft()
		{
			foreach (var view in _elementViews.Values)
			{
				view.Move(view.GetPosition() - new Vector3(_speed * Time.deltaTime, 0, 0));
			}
		}
	}
}