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
		[SerializeField]
		private TextView _comboTextView;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterEntryPoint<ScorePresenter>(resolver =>
				new ScorePresenter(_scoreTextView, resolver.Resolve<ScoreSystem>()), Lifetime.Singleton);

			builder.RegisterEntryPoint<ComboPresenter>(resolver =>
				new ComboPresenter(_comboTextView, resolver.Resolve<ScoreSystem>()), Lifetime.Singleton);
		}
	}
}