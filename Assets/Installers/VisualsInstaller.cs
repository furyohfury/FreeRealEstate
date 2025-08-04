using Game.BeatmapControl;
using Game.Services;
using Game.SongMapTime;
using Game.Visuals;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
	public sealed class VisualsInstaller : LifetimeScope
	{
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