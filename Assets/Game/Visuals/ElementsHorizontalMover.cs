using System.Collections.Generic;
using Game.Services;
using ObservableCollections;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Game.Visuals
{
	public sealed class ElementsHorizontalMover : IInitializable
	{
		private readonly ScreenBeatmapBoundsService _boundsService;
		private readonly ElementViewsRegistry _registry;

		private float _speed;
		private readonly CompositeDisposable _disposable = new();

		public ElementsHorizontalMover(ScreenBeatmapBoundsService boundsService, ElementViewsRegistry registry)
		{
			_boundsService = boundsService;
			_registry = registry;
		}

		public void Initialize()
		{
			_speed = CalculateSpeed();
			_registry.ActiveElements.ObserveAdd()
			         .Select(@event => @event.Value.Value)
			         .Subscribe(view => view.Move(_boundsService.StartPoint))
			         .AddTo(_disposable);

			Observable.EveryUpdate()
			          .Subscribe(_ => MoveViewsToLeft())
			          .AddTo(_disposable);
		}

		private float CalculateSpeed()
		{
			var distance = Vector2.Distance(_boundsService.HitPoint, _boundsService.StartPoint);
			var speed = distance / NotesVisualStaticData.SCROLL_TIME;
			return speed;
		}

		private void MoveViewsToLeft()
		{
			ICollection<ElementView> elementViews = _registry.ElementViews;

			foreach (var view in elementViews)
			{
				view.Move(view.GetPosition() - new Vector3(_speed * Time.deltaTime, 0, 0));
			}
		}
	}
}