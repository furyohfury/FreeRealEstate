using Game;
using Game.BeatmapControl;
using Game.ElementHandle;
using Game.Input;
using Game.Scoring;
using Game.SongMapTime;
using ObjectProvide;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
	public class SceneInstaller : LifetimeScope
	{
		[SerializeField]
		private PointsForStatusConfig _pointsForStatusConfig;
		[SerializeField]
		private DrumrollTickRateConfig _drumrollTickrateConfig;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<AddressablesObjectProvider>(Lifetime.Singleton)
			       .AsImplementedInterfaces();

			builder.Register<MapTime>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
			builder.Register<BeatmapLauncher>(Lifetime.Singleton);
			builder.Register<BeatmapPipeline>(Lifetime.Singleton);

			builder.RegisterEntryPoint<SingleNoteTimeoutSwitcher>();
			builder.RegisterEntryPoint<SpinnerTimeoutSwitcher>();
			builder.RegisterEntryPoint<ElementOnStatusSwitcher>();
			builder.RegisterEntryPoint<DrumrollTimeoutSwitcher>();

			builder.Register<ElementClickStrategy, SingleNoteClickStrategy>(Lifetime.Scoped);
			builder.Register<ElementClickStrategy, SpinnerClickStrategy>(Lifetime.Scoped);
			builder.Register<DrumrollTickrateService>(Lifetime.Singleton);
			builder.RegisterInstance<DrumrollTickRateConfig>(_drumrollTickrateConfig);
			builder.Register<ElementClickStrategy, DrumrollClickStrategy>(Lifetime.Scoped);
			builder.Register<ElementsClickHandler>(Lifetime.Singleton);
			builder.Register<DifficultySwitcher>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<InputReader>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
			builder.Register<InputActions>(Lifetime.Singleton);

			builder.RegisterEntryPoint<InputNotesController>();

			builder.Register<MapScore>(Lifetime.Singleton);
			builder.RegisterEntryPoint<MapScoreController>();
			builder.RegisterEntryPoint<MapScoreResetter>();
			builder.RegisterInstance<PointsForStatusConfig>(_pointsForStatusConfig);

			Debug.Log("Successfully installed all scene systems");
		}
	}
}