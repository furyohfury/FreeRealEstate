using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class UIInstaller : MonoInstaller
	{
		[SerializeField]
		private TextFieldView _pointsTextField;
		[SerializeField] 
		private TextFieldView _pikminCount;
		[SerializeField]
		private Canvas _uiCanvas;
		[SerializeField]
		private GameObject _shipIcon;
		[SerializeField] 
		private GameObject _finalCarriable;

		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<ShipPointsPresenter>()
			         .AsSingle()
			         .WithArguments(_pointsTextField);

			Container.Bind<Canvas>()
			         .FromInstance(_uiCanvas);

			Container.BindInterfacesAndSelfTo<ShipVisibilityChecker>()
			         .AsSingle()
			         .WithArguments(_shipIcon);

			Container.BindInterfacesAndSelfTo<PikminCountPresenter>()
			         .AsSingle()
			         .WithArguments(_pikminCount);

			Container.Bind<GameOverScreen>()
			         .FromComponentInHierarchy()
			         .AsSingle();

			Container.BindInterfacesAndSelfTo<GameOverObserver>()
			         .AsSingle()
			         .WithArguments(_finalCarriable);
		}
	}
}