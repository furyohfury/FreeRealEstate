using Game.Scoring;
using Game.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
	public sealed class UIInstaller : LifetimeScope
	{
		[SerializeField]
		private TextView _scoreTextView;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<ScorePresenter>(resolver =>
			       {
				       var mapScore = resolver.Resolve<MapScore>();
				       return new ScorePresenter(mapScore, _scoreTextView);
			       }, Lifetime.Singleton)
			       .AsImplementedInterfaces();
		}
	}
}