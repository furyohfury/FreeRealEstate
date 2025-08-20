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
			builder.RegisterEntryPoint<ScorePresenter>(resolver =>
				new ScorePresenter(_scoreTextView, resolver.Resolve<ScoreSystem>()), Lifetime.Singleton);
		}
	}
}