using Beatmaps;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class ActiveSpinnerFactory : IActiveSpinnerFactory
	{
		private readonly IPrefabFactory _prefabFactory;
		private readonly Transform _container;
		private readonly ActiveSpinnerPresenterFactory _activeSpinnerPresenterFactory;
		private ActiveSpinnerView _activeSpinnerView;
		private ActiveSpinnerPresenter _activeSpinnerPresenter;

		public ActiveSpinnerFactory(
			IPrefabFactory prefabFactory,
			ActiveSpinnerPresenterFactory activeSpinnerPresenterFactory,
			Transform container
			)
		{
			_prefabFactory = prefabFactory;
			_container = container;
			_activeSpinnerPresenterFactory = activeSpinnerPresenterFactory;
		}

		public async void CreateActiveSpinner(Spinner spinner)
		{
			Debug.Log("Spawn spinner");
			_activeSpinnerView = await _prefabFactory.Spawn<ActiveSpinnerView>(PrefabsStaticNames.ACTIVE_SPINNER, _container);
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