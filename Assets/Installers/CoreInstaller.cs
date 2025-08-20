using Game;
using Game.BeatmapControl;
using Game.BeatmapLaunch;
using Game.BeatmapTime;
using Game.Controllers;
using Game.ElementHandle;
using Game.Input;
using Game.Scoring;
using Game.Services;
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
		[SerializeField]
		private Transform _clickedNotesEndPoint;
		[SerializeField] [Required]
		private Transform _activeSpinnerContainer;
		[SerializeField] [Required]
		private ActiveSpinnerView _activeSpinnerViewPrefab;
		[SerializeField]
		private DrumrollNoteViewIdConfig _drumrollNoteViewIdConfig;
		[SerializeField]
		private ActiveSpinnerIdConfig _activeSpinnerIdConfig;
		[SerializeField]
		private JudgementSettings _judgementSettings;
		[SerializeField]
		private ScoreResultConfig _scoreResultConfig;

		protected override void Configure(IContainerBuilder builder)
		{
			// Map time
			builder.Register<MapTime>(Lifetime.Singleton).As<IMapTime>();
			builder.RegisterEntryPoint<MapTimeController>();

			builder.Register<BeatmapPipeline>(Lifetime.Singleton);

			RegisterCoreServices(builder);

			builder.Register<IElementTimeoutCalculator, SingleNoteTimeoutCalculator>(Lifetime.Singleton);
			builder.Register<IElementTimeoutCalculator, SpinnerTimeoutCalculator>(Lifetime.Singleton);
			builder.Register<IElementTimeoutCalculator, DrumrollTimeoutCalculator>(Lifetime.Singleton);
			builder.Register<ElementTimeoutHelper>(Lifetime.Singleton);
			builder.RegisterEntryPoint<ElementOnStatusSwitcher>();


			builder.Register<DifficultySwitcher>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			RegisterElementHandlers(builder);
			RegisterInputSystems(builder);
			RegisterScoreSystems(builder);
			RegisterLaunchers(builder);
			Debug.Log("Successfully installed all core systems");

			RegisterVisualSystems(builder);
		}

		private void RegisterCoreServices(IContainerBuilder builder)
		{
			builder.RegisterInstance<DrumrollTickRateConfig>(_drumrollTickrateConfig);
			builder.Register<DrumrollTickrateService>(Lifetime.Singleton);
		}

		private void RegisterScoreSystems(IContainerBuilder builder)
		{
			builder.Register<MapScore>(Lifetime.Singleton);
		}

		private static void RegisterInputSystems(IContainerBuilder builder)
		{
			builder.Register<InputReader>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
			builder.Register<InputActions>(Lifetime.Singleton);

			builder.RegisterEntryPoint<InputNotesController>();
		}

		private void RegisterElementHandlers(IContainerBuilder builder)
		{
			builder.Register<ElementTimeoutObservable>(Lifetime.Singleton)
			       .AsImplementedInterfaces();

			builder.Register<ElementClickStrategy, SingleNoteClickStrategy>(Lifetime.Scoped);
			builder.Register<ElementClickStrategy, SpinnerClickStrategy>(Lifetime.Scoped);
			builder.Register<ElementClickStrategy, DrumrollClickStrategy>(Lifetime.Scoped);
			builder.Register<ClickHandleResultStrategy>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<TimeoutHandleResultStrategy>(Lifetime.Singleton)
			       .AsImplementedInterfaces();
			builder.Register<HandleResultObservable>(Lifetime.Singleton)
			       .AsImplementedInterfaces();
			builder.Register<SpinnerStartObservable>(Lifetime.Singleton)
			       .AsImplementedInterfaces();

			builder.RegisterEntryPoint<ElementOnTimeoutSwitcher>();
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
			// Scene bounds and container
			builder.RegisterInstance<ScreenBeatmapBoundsService>(new ScreenBeatmapBoundsService(_startPoint, _endPoint));
			builder.RegisterInstance<ElementViewContainerService>(new ElementViewContainerService(_notesContainer));

			// Element view factory
			builder.RegisterInstance<SingleNotePrefabConfig>(_singleNotePrefabConfig);
			builder.RegisterInstance<SpinnerPrefabConfig>(_spinnerPrefabConfig);
			builder.RegisterInstance<DrumrollPrefabConfig>(_drumrollPrefabConfig);
			builder.Register<SingleNoteViewFactory>(Lifetime.Singleton)
			       .AsImplementedInterfaces();
			builder.Register<SpinnerViewFactory>(Lifetime.Singleton)
			       .AsImplementedInterfaces();
			builder.Register<DrumRollViewFactory>(Lifetime.Singleton)
			       .AsImplementedInterfaces();
			builder.Register<ElementViewFactory>(Lifetime.Singleton);

			builder.Register<ElementsVisualsSpawner>((resolver) =>
			       {
				       var viewFactory = resolver.Resolve<ElementViewFactory>();
				       var mapTime = resolver.Resolve<IMapTime>();
				       var elementViewContainerService = resolver.Resolve<ElementViewContainerService>();
				       return new ElementsVisualsSpawner(
					       viewFactory,
					       mapTime,
					       elementViewContainerService
				       );
			       }, Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			// Notes mover
			builder.Register<ElementsHorizontalMover>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			// View registry
			builder.Register<ElementViewsRegistry>(Lifetime.Singleton);
			builder.RegisterEntryPoint<ViewsRegistryAddController>();

			// Result visual handlers
			builder.Register<SingleNoteVisualClickHandler>(resolver =>
			       {
				       var registry = resolver.Resolve<ElementViewsRegistry>();
				       return new SingleNoteVisualClickHandler(_clickedNotesEndPoint, registry);
			       }, Lifetime.Singleton)
			       .As<IVisualClickHandler>();

			builder.RegisterInstance<DrumrollNoteViewIdConfig>(_drumrollNoteViewIdConfig)
			       .As<PrefabIdConfig<DrumrollNoteView>>();

			builder.Register<PrefabFactory<DrumrollNoteView>>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<DrumrollVisualClickHandler>(
				       resolver => new DrumrollVisualClickHandler(
					       resolver.Resolve<PrefabFactory<DrumrollNoteView>>(),
					       _clickedNotesEndPoint,
					       _notesContainer,
					       _endPoint
				       ),
				       Lifetime.Singleton)
			       .As<IVisualClickHandler>();


			// Active spinner systems
			builder.RegisterInstance<ActiveSpinnerView>(_activeSpinnerViewPrefab);
			builder.RegisterInstance<ActiveSpinnerIdConfig>(_activeSpinnerIdConfig).As<PrefabIdConfig<ActiveSpinnerView>>();
			builder.Register<PrefabFactory<ActiveSpinnerView>>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
			builder.Register<ActiveSpinnerPresenterFactory>(Lifetime.Singleton);
			builder.Register<ActiveSpinnerController>(resolver =>
			       {
				       var activeSpinnerFactory = resolver.Resolve<PrefabFactory<ActiveSpinnerView>>();
				       var activeSpinnerPresenterFactory = resolver.Resolve<ActiveSpinnerPresenterFactory>();
				       return new ActiveSpinnerController(activeSpinnerFactory,
					       activeSpinnerPresenterFactory,
					       _activeSpinnerContainer);
			       }, Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<SpinnerVisualClickHandler>(resolver => new SpinnerVisualClickHandler(
					       _activeSpinnerContainer,
					       resolver.Resolve<ElementViewsRegistry>(),
					       resolver.Resolve<ActiveSpinnerController>()),
				       Lifetime.Singleton)
			       .As<IVisualClickHandler>();

			builder.Register<ElementHandleVisualSystem>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			Debug.Log("Successfully installed all visual systems");

			// Score system
			builder.RegisterInstance<JudgementSettings>(_judgementSettings);
			builder.RegisterInstance<ScoreResultConfig>(_scoreResultConfig);
			builder.Register<MapScore>(Lifetime.Singleton).As<IMapScore>();
			builder.Register<Combo>(Lifetime.Singleton).As<ICombo>();
			builder.RegisterInstance<LinearScoreCalculator>(
				       new LinearScoreCalculator(_judgementSettings, _scoreResultConfig.ScoreResults))
			       .As<IScoreCalculator>();
			builder.Register<ScoreSystem>(Lifetime.Singleton);
			builder.RegisterEntryPoint<ScoreSystemController>();
		}
	}
}