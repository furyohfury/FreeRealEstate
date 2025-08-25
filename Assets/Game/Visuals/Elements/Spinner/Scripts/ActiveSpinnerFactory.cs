using Beatmaps;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class ActiveSpinnerFactory : IActiveSpinnerFactory
	{
		private readonly PrefabFactory<ActiveSpinnerView> _activeSpinnerFactory;
		private readonly Transform _container;
		private readonly ActiveSpinnerPresenterFactory _activeSpinnerPresenterFactory;
		private ActiveSpinnerView _activeSpinnerView;
		private ActiveSpinnerPresenter _activeSpinnerPresenter;

		public ActiveSpinnerFactory(
			PrefabFactory<ActiveSpinnerView> activeSpinnerFactory,
			ActiveSpinnerPresenterFactory activeSpinnerPresenterFactory,
			Transform container
		)
		{
			_activeSpinnerFactory = activeSpinnerFactory;
			_container = container;
			_activeSpinnerPresenterFactory = activeSpinnerPresenterFactory;
		}

		public void CreateActiveSpinner(Spinner spinner)
		{
			Debug.Log("Spawn spinner");
			_activeSpinnerView = _activeSpinnerFactory.Spawn(_container);
			_activeSpinnerPresenter = _activeSpinnerPresenterFactory.Create(spinner, _activeSpinnerView);
		}

		public void RemoveCurrent()
		{
			if (_activeSpinnerView != null)
			{
				_activeSpinnerView.Destroy();
			}

			_activeSpinnerPresenter?.Dispose();
		}
	}
}