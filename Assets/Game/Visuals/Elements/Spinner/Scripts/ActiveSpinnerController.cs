using System;
using Beatmaps;
using Game.BeatmapControl;
using Game.BeatmapTime;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Game.Visuals
{
	public sealed class ActiveSpinnerController : IInitializable, IDisposable
	{
		private readonly IMapTime _mapTime;
		private readonly BeatmapPipeline _beatmapPipeline;
		private readonly ActiveSpinnerFactory _activeSpinnerFactory;
		private readonly Transform _container;
		private readonly ActiveSpinnerPresenterFactory _activeSpinnerPresenterFactory;
		private IDisposable _disposable;
		private readonly SerialDisposable _activePresenterDisposable = new();

		public ActiveSpinnerController(
			IMapTime mapTime,
			BeatmapPipeline beatmapPipeline,
			ActiveSpinnerFactory activeSpinnerFactory,
			ActiveSpinnerPresenterFactory activeSpinnerPresenterFactory,
			Transform container
		)
		{
			_mapTime = mapTime;
			_activeSpinnerFactory = activeSpinnerFactory;
			_beatmapPipeline = beatmapPipeline;
			_container = container;
			_activeSpinnerPresenterFactory = activeSpinnerPresenterFactory;
		}

		public void Initialize()
		{
			_disposable = _beatmapPipeline.Element
			                              // .Where(element => element is Spinner)
			                              // .Cast<MapElement, Spinner>()
			                              .OfType<MapElement, Spinner>()
			                              .SelectMany(spinner =>
				                              Observable.Timer(
					                                        TimeSpan.FromSeconds(spinner.HitTime - _mapTime.GetMapTimeInSeconds())
				                                        )
				                                        .Select(_ => spinner)
			                              )
			                              .Subscribe(CreateActiveSpinnerView);
		}

		private void CreateActiveSpinnerView(Spinner spinner)
		{
			Debug.Log("Spawn spinner");
			var activeSpinnerView = _activeSpinnerFactory.Spawn(_container);
			var presenter = _activeSpinnerPresenterFactory.Create(spinner, activeSpinnerView);

			var spinnerLifeTime = TimeSpan.FromSeconds(spinner.Duration);
			_activePresenterDisposable.Disposable = Observable.Timer(spinnerLifeTime)
			                                                  .Subscribe(_ => presenter.Dispose());
		}

		public void Dispose()
		{
			_disposable.Dispose();
			_activePresenterDisposable.Dispose();
		}
	}
}