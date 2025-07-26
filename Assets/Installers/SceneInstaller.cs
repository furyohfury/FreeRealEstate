using Game;
using Game.ElementHandle;
using Game.Input;
using Game.Scoring;
using Game.SongMapTime;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
	public class SceneInstaller : LifetimeScope
	{
		[SerializeField]
		private PointsForStatusConfig _pointsForStatusConfig;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<ActiveElementService>(Lifetime.Singleton);
			builder.Register<ActiveMapService>(Lifetime.Singleton);
			builder.Register<ActiveElementIndexService>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<ActiveElementSwitcher>(Lifetime.Singleton);
			builder.Register<ActiveElementTimeoutSwitcher>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
			builder.Register<ActiveElementOnClickSwitcher>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
			builder.Register<MapChangeObserver>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<ElementClickStrategy, SingleNoteClickStrategy>(Lifetime.Scoped);
			builder.Register<ElementClickStrategy, SpinnerClickStrategy>(Lifetime.Scoped);
			builder.Register<ElementsClickHandler>(Lifetime.Singleton);
			builder.Register<DifficultySwitcher>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<IMapTime, MapTime>(Lifetime.Singleton);
			builder.Register<BeatmapLauncher>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

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