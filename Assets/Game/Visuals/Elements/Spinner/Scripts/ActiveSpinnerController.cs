using System;
using Beatmaps;
using R3;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class ActiveSpinnerController : IDisposable, IActiveSpinnerController
	{
		private readonly PrefabFactory<ActiveSpinnerView> _activeSpinnerFactory;
		private readonly Transform _container;
		private readonly ActiveSpinnerPresenterFactory _activeSpinnerPresenterFactory;
		private IDisposable _disposable;
		private readonly SerialDisposable _activePresenterDisposable = new();

		public ActiveSpinnerController(
			PrefabFactory<ActiveSpinnerView> activeSpinnerFactory,
			ActiveSpinnerPresenterFactory activeSpinnerPresenterFactory,
			Transform container
		)
		{
			_activeSpinnerFactory = activeSpinnerFactory;
			_container = container;
			_activeSpinnerPresenterFactory = activeSpinnerPresenterFactory;
		}

		public void CreateActiveSpinnerView(Spinner spinner)
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