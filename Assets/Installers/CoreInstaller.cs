using Game;
using Game.BeatmapControl;
using Game.BeatmapLaunch;
using Game.ElementHandle;
using Game.Input;
using Game.Scoring;
using Game.Services;
using Game.SongMapTime;
using Game.Visuals;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
	public class CoreInstaller : LifetimeScope
	{
		[Header("Core")]
		[SerializeField]
		private PointsForStatusConfig _pointsForStatusConfig;
		[SerializeField]
		private DrumrollTickRateConfig _drumrollTickrateConfig;

		[Header("Visuals")]
		[SerializeField] [Required]
		private SingleNotePrefabConfig _singleNotePrefabConfig;
		[SerializeField] [Required]
		private SpinnerPrefabConfig _spinnerPrefabConfig;
		[SerializeField] [Required]
		private DrumrollPrefabConfig _drumrollPrefabConfig;
		[SerializeField] [Required]
		private Transform _notesContainer;
		[SerializeField] [Required]
		private Transform _startPoint;
		[SerializeField] [Required]
		private Transform _endPoint;
		[SerializeField] [Required]
		private Transform _activeSpinnerContainer;
		[SerializeField] [Required]
		private ActiveSpinnerView _activeSpinnerViewPrefab;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<MapTime>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
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

			RegisterLaunchers(builder);

			Debug.Log("Successfully installed all scene systems");

			RegisterVisualSystems(builder);
		}

		private static void RegisterLaunchers(IContainerBuilder builder)
		{
			builder.Register<BeatmapTimeLauncher>(Lifetime.Singleton)
			       .AsImplementedInterfaces();
			builder.Register<BeatmapAudioLauncher>(Lifetime.Singleton)
			       .AsImplementedInterfaces();
			builder.Register<BeatmapBackgroundLauncher>(Lifetime.Singleton)
			       .AsImplementedInterfaces();
			builder.Register<BeatmapElementsVisualLauncher>(Lifetime.Singleton)
			       .AsImplementedInterfaces();
			builder.Register<BeatmapPipelineLauncher>(Lifetime.Singleton)
			       .AsImplementedInterfaces();
			builder.Register<BeatmapLauncher>(Lifetime.Singleton);
		}

		private void RegisterVisualSystems(IContainerBuilder builder)
		{
			builder.RegisterInstance<NotesLineBoundsService>(new NotesLineBoundsService(_startPoint, _endPoint, _notesContainer));
			builder.Register<NotesLineMover>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.RegisterInstance<SingleNotePrefabConfig>(_singleNotePrefabConfig);
			builder.RegisterInstance<SpinnerPrefabConfig>(_spinnerPrefabConfig);
			builder.RegisterInstance<DrumrollPrefabConfig>(_drumrollPrefabConfig);
			builder.Register<SingleNoteViewFactory>(Lifetime.Singleton).AsImplementedInterfaces();
			builder.Register<SpinnerViewFactory>(Lifetime.Singleton).AsImplementedInterfaces();
			builder.Register<DrumRollViewFactory>(Lifetime.Singleton).AsImplementedInterfaces();
			builder.Register<ElementViewFactory>(Lifetime.Singleton);

			builder.Register<NotesVisualSystem>((resolver) =>
			{
				var viewFactory = resolver.Resolve<ElementViewFactory>();
				var mapTime = resolver.Resolve<IMapTime>();
				var notesLineBoundsService = resolver.Resolve<NotesLineBoundsService>();
				var notesLineMover = resolver.Resolve<NotesLineMover>();
				return new NotesVisualSystem(
					viewFactory,
					mapTime,
					notesLineBoundsService,
					notesLineMover
				);
			}, Lifetime.Singleton);

			builder.RegisterInstance<ActiveSpinnerView>(_activeSpinnerViewPrefab);
			builder.Register<ActiveSpinnerFactory>(Lifetime.Singleton);
			builder.Register<ActiveSpinnerPresenterFactory>(Lifetime.Singleton);
			builder.Register<ActiveSpinnerController>(resolver =>
			       {
				       var mapTime = resolver.Resolve<IMapTime>();
				       var beatmapPipeline = resolver.Resolve<BeatmapPipeline>();
				       var activeSpinnerFactory = resolver.Resolve<ActiveSpinnerFactory>();
				       var activeSpinnerPresenterFactory = resolver.Resolve<ActiveSpinnerPresenterFactory>();
				       return new ActiveSpinnerController(
					       mapTime,
					       beatmapPipeline,
					       activeSpinnerFactory,
					       activeSpinnerPresenterFactory,
					       _activeSpinnerContainer);
			       }, Lifetime.Singleton)
			       .AsImplementedInterfaces();

			Debug.Log("Successfully installed all visual systems");
		}
	}
}