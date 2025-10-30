using Game.UI;
using Gameplay;
using UnityEngine;
using Zenject;

namespace Installers
{
	public sealed class UiInstaller : MonoInstaller
	{
		[SerializeField]
		private ScrollTransitionText _scorePlayerOneText;
		[SerializeField]
		private ScrollTransitionText _scorePlayerTwoText;

		public override void InstallBindings()
		{
			Container.BindInterfacesTo<ScoreUIObserver>()
			         .AsCached()
			         .WithArguments(_scorePlayerOneText, Player.One);

			Container.BindInterfacesTo<ScoreUIObserver>()
			         .AsCached()
			         .WithArguments(_scorePlayerTwoText, Player.Two);

			Container.Bind<WinPopup>()
			         .FromComponentInHierarchy()
			         .AsSingle();

			Container.Bind<LosePopup>()
			         .FromComponentInHierarchy()
			         .AsSingle();

			Container.BindInterfacesTo<GameOverPopupPresenter>()
			         .AsSingle();

			Container.Bind<ScoreView>()
			         .FromComponentInHierarchy()
			         .AsSingle();

			Container.BindInterfacesTo<ScoreViewPresenter>()
			         .AsSingle();
		}
	}
}