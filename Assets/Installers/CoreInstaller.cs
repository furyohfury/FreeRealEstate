using Game;
using Game.BeatmapControl;
using Game.BeatmapFinish;
using Game.BeatmapLaunch;
using Game.BeatmapRestart;
using Game.BeatmapTime;
using Game.Controllers;
using Game.ElementHandle;
using Game.Input;
using Game.SceneSwitch;
using Game.Scoring;
using Game.Services;
using Game.UI;
using Game.UI.Leaderboards;
using Game.UI.Navigation;
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
		[SerializeField] [Required]
		private DrumrollTickRateConfig _drumrollTickrateConfig;

		[Header("Visuals")]
		[SerializeField] [Required]
		private Transform _notesContainer;
		[SerializeField] [Required]
		private Transform _startPoint;
		[SerializeField] [Required]
		private Transform _endPoint;
		[SerializeField] [Required]
		private Transform _clickedNotesEndPoint;
		[SerializeField] [Required]
		private Transform _activeSpinnerContainer;
		[SerializeField] [Required]
		private Transform _popupContainer;
		[SerializeField] [Required]
		private ActiveSpinnerView _activeSpinnerViewPrefab;
		[SerializeField] [Required]
		private JudgementSettings _judgementSettings;
		[SerializeField] [Required]
		private ScoreResultConfig _scoreResultConfig;
		[SerializeField] [Required]
		private TextView _scoreTextView;
		[SerializeField] [Required]
		private TextView _comboTextView;
		[SerializeField] [Required]
		private ScreenInputNotesObservable _screenInputNotesObservable;
		[SerializeField] [Required]
		private ButtonView _backButton;
		[SerializeField] [Required]
		private ButtonView _restartButton;
		[SerializeField] [Required]
		private ConstantSizePicture _background;
		[SerializeField] 
		private Camera _camera;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance<Camera>(_camera);
			RegisterCoreServices(builder);
			builder.Register<MapTime>(Lifetime.Singleton).As<IMapTime>();
			builder.RegisterEntryPoint<MapTimeController>();

			builder.Register<BeatmapPipeline>(Lifetime.Singleton);


			builder.RegisterEntryPoint<ElementOnStatusSwitcher>();

			builder.Register<DifficultySwitcher>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			RegisterElementHandlers(builder);
			RegisterInputSystems(builder);
			RegisterVisualSystems(builder);
			RegisterUISystems(builder);
			RegisterBeatmapLauncher(builder);
			RegisterBeatmapRestarter(builder);
			RegisterBeatmapFinisher(builder);
			RegisterCurrentBundleService(builder);

			Debug.Log("Successfully installed all scene systems");
		}

		private static void RegisterBeatmapFinisher(IContainerBuilder builder)
		{
			builder.Register<AudioBeatmapFinishable>(Lifetime.Singleton)
			       .As<IBeatmapFinishable>();
			builder.Register<MapTimeBeatmapFinishable>(Lifetime.Singleton)
			       .As<IBeatmapFinishable>();
			builder.Register<LeaderboardBeatmapFinishable>(Lifetime.Singleton)
			       .As<IBeatmapFinishable>();
			builder.Register<BeatmapFinisher>(Lifetime.Singleton);
			builder.Register<BeatmapEndObservable>(Lifetime.Singleton)
			       .AsImplementedInterfaces();
			builder.Register<BeatmapFinishObserver>(Lifetime.Singleton)
			       .AsImplementedInterfaces();
		}

		private void RegisterUISystems(IContainerBuilder builder)
		{
			builder.RegisterEntryPoint<ScorePresenter>(resolver =>
				new ScorePresenter(_scoreTextView, resolver.Resolve<ScoreSystem>()), Lifetime.Singleton);

			builder.RegisterEntryPoint<ComboPresenter>(resolver =>
				new ComboPresenter(_comboTextView, resolver.Resolve<ScoreSystem>()), Lifetime.Singleton);

			builder.Register<LeaderboardFactory>(Lifetime.Singleton);

			builder.Register<WindowSystem>(resolver =>
					       new WindowSystem(resolver.Resolve<IPrefabFactory>(), _popupContainer)
				       , Lifetime.Singleton)
			       .As<IWindowSystem>();

			builder.Register<LeaderboardPresenter>(Lifetime.Transient);
			builder.Register<LeaderboardPresenterFactory>(Lifetime.Singleton);

			builder.RegisterEntryPoint<BackButtonPresenter>(resolver =>
					new BackButtonPresenter(
						_backButton,
						new MainMenuSceneSwitchable()
						)
				, Lifetime.Scoped);

			builder.RegisterEntryPoint<RestartButtonPresenter>(resolver =>
					new RestartButtonPresenter(
						_restartButton,
						resolver.Resolve<BeatmapRestarter>()
						)
				, Lifetime.Scoped);

			Debug.Log("Successfully installed UI scene systems");
		}

		private static void RegisterBeatmapRestarter(IContainerBuilder builder)
		{
			builder.Register<BeatmapAudioRestartable>(Lifetime.Singleton).As<IBeatmapRestartable>();
			builder.Register<BeatmapPipelineRestartable>(Lifetime.Singleton).As<IBeatmapRestartable>();
			builder.Register<BeatmapScoreRestartable>(Lifetime.Singleton).As<IBeatmapRestartable>();
			builder.Register<BeatmapTimeRestartable>(Lifetime.Singleton).As<IBeatmapRestartable>();
			builder.Register<BeatmapVisualMoverRestartable>(Lifetime.Singleton).As<IBeatmapRestartable>();
			builder.Register<BeatmapVisualRegistryRestartable>(Lifetime.Singleton).As<IBeatmapRestartable>();
			builder.Register<BeatmapVisualSpawnerRestartable>(Lifetime.Singleton).As<IBeatmapRestartable>();
			builder.Register<BeatmapActiveSpinnerRestartable>(Lifetime.Singleton).As<IBeatmapRestartable>();
			builder.Register<BeatmapSpinnerClickRestartable>(Lifetime.Singleton).As<IBeatmapRestartable>();
			builder.Register<BeatmapRestarter>(Lifetime.Singleton);
		}

		private static void RegisterCurrentBundleService(IContainerBuilder builder)
		{
			builder.Register<CurrentBundleService>(Lifetime.Singleton);
			builder.RegisterEntryPoint<BundleServiceController>();
		}

		private void RegisterCoreServices(IContainerBuilder builder)
		{
			builder.RegisterInstance<DrumrollTickRateConfig>(_drumrollTickrateConfig);
			builder.Register<DrumrollTickrateService>(Lifetime.Singleton);
		}

		private void RegisterInputSystems(IContainerBuilder builder)
		{
			builder.Register<InputActions>(Lifetime.Singleton);

			builder.Register<KeysInputNotesObservable>(Lifetime.Singleton)
			       .AsImplementedInterfaces();

			builder.RegisterInstance<ScreenInputNotesObservable>(_screenInputNotesObservable)
			       .As<IInputNotesObservable>();

			builder.Register<InputRestartable>(Lifetime.Scoped)
			       .AsImplementedInterfaces();

			builder.Register<InputReader>(Lifetime.Singleton)
			       .AsImplementedInterfaces();

			builder.RegisterEntryPoint<InputNotesController>();

			builder.RegisterEntryPoint<InputRestartObserver>();
		}

		private void RegisterElementHandlers(IContainerBuilder builder)
		{
			builder.Register<ElementTimeoutObservable>(Lifetime.Singleton)
			       .AsImplementedInterfaces();

			builder.Register<SingleNoteClickStrategy>(Lifetime.Singleton).As<ElementClickStrategy>();
			builder.Register<SpinnerClickStrategy>(Lifetime.Singleton).As<ElementClickStrategy>().AsSelf();
			builder.Register<DrumrollClickStrategy>(Lifetime.Singleton).As<ElementClickStrategy>();
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

		private static void RegisterBeatmapLauncher(IContainerBuilder builder)
		{
			builder.Register<BeatmapTimeLauncher>(Lifetime.Singleton)
			       .As<IBeatmapLaunchable>();
			builder.Register<WindowsBeatmapLaunchable>(Lifetime.Singleton)
			       .As<IBeatmapLaunchable>();
			builder.Register<BeatmapAudioLauncher>(Lifetime.Singleton)
			       .As<IBeatmapLaunchable>();
			builder.Register<BeatmapBackgroundLauncher>(Lifetime.Singleton)
			       .As<IBeatmapLaunchable>();
			builder.Register<BeatmapElementsVisualLauncher>(Lifetime.Singleton)
			       .As<IBeatmapLaunchable>();
			builder.Register<BeatmapPipelineLauncher>(Lifetime.Singleton)
			       .As<IBeatmapLaunchable>();
			builder.Register<BeatmapLauncher>(Lifetime.Singleton);
		}

		private void RegisterVisualSystems(IContainerBuilder builder)
		{
			// Scene bounds and container
			builder.RegisterInstance<ScreenBeatmapBoundsService>(new ScreenBeatmapBoundsService(_startPoint, _endPoint));
			builder.RegisterInstance<ElementViewContainerService>(new ElementViewContainerService(_notesContainer));

			// Element view factory
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

			builder.RegisterEntryPoint<HorizontalMoverController>();

			// View registry
			builder.Register<ElementViewsRegistry>(Lifetime.Singleton);
			builder.RegisterEntryPoint<ViewsRegistryAddController>();

			// Destroyer
			builder.Register<ElementViewDestroyer>(Lifetime.Singleton).As<IElementViewDestroyer>();

			// Result visual handlers
			builder.Register<SingleNoteVisualClickHandler>(resolver =>
			       {
				       var registry = resolver.Resolve<ElementViewsRegistry>();
				       var destroyer = resolver.Resolve<IElementViewDestroyer>();
				       return new SingleNoteVisualClickHandler(_clickedNotesEndPoint, registry, destroyer);
			       }, Lifetime.Singleton)
			       .As<IVisualClickHandler>();

			builder.Register<DrumrollVisualClickHandler>(
				       resolver => new DrumrollVisualClickHandler(
					       resolver.Resolve<IPrefabFactory>(),
					       _clickedNotesEndPoint,
					       _notesContainer,
					       _endPoint,
					       resolver.Resolve<IElementViewDestroyer>()
					       ),
				       Lifetime.Singleton)
			       .As<IVisualClickHandler>();


			// Active spinner systems
			builder.Register<ActiveSpinnerPresenterFactory>(Lifetime.Singleton);
			builder.Register<ActiveSpinnerFactory>(resolver =>
			       {
				       var activeSpinnerFactory = resolver.Resolve<IPrefabFactory>();
				       var activeSpinnerPresenterFactory = resolver.Resolve<ActiveSpinnerPresenterFactory>();
				       return new ActiveSpinnerFactory(activeSpinnerFactory,
					       activeSpinnerPresenterFactory,
					       _activeSpinnerContainer);
			       }, Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<SpinnerVisualClickHandler>(resolver => new SpinnerVisualClickHandler(
					       _activeSpinnerContainer,
					       resolver.Resolve<ElementViewsRegistry>(),
					       resolver.Resolve<ActiveSpinnerFactory>(),
					       resolver.Resolve<IElementViewDestroyer>()),
				       Lifetime.Singleton)
			       .As<IVisualClickHandler>();

			builder.Register<ElementHandleVisualSystem>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			// Score system
			builder.RegisterInstance<JudgementSettings>(_judgementSettings);
			builder.RegisterInstance<ScoreResultConfig>(_scoreResultConfig);
			builder.Register<MapScore>(Lifetime.Singleton).As<IMapScore>();
			builder.Register<Combo>(Lifetime.Singleton).As<ICombo>();
			builder.RegisterInstance<LinearScoreCalculator>(
				       new LinearScoreCalculator(_judgementSettings, _scoreResultConfig.ScoreResults))
			       .As<IScoreCalculator>();
			builder.Register<ScoreSystem>(Lifetime.Singleton);
			builder.RegisterEntryPoint<ScoreController>();
			builder.RegisterEntryPoint<ComboController>();

			// Background
			builder.RegisterInstance<BackgroundChanger>(new BackgroundChanger(_background))
			       .As<IBackgroundChanger>();

			Debug.Log("Successfully installed scene visual systems");
		}
	}
}