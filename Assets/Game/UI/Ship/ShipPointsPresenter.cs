using System;
using R3;
using Zenject;

namespace Game
{
	public sealed class ShipPointsPresenter : IInitializable, IDisposable
	{
		private readonly TextFieldView _textFieldView;
		private readonly ShipPoints _shipPoints;
		private readonly CompositeDisposable _disposable = new();

		public ShipPointsPresenter(TextFieldView textFieldView, ShipPoints shipPoints)
		{
			_textFieldView = textFieldView;
			_shipPoints = shipPoints;
		}

		public void Initialize()
		{
			_shipPoints.Points
			           .Subscribe(OnPointsChanged)
			           .AddTo(_disposable);
		}

		private void OnPointsChanged(int points)
		{
			_textFieldView.SetText(points.ToString());
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}