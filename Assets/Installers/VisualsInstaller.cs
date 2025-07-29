using Game.BeatmapControl;
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
		private Transform _container;
		[SerializeField] [Required]
		private Transform _startPoint;
		[SerializeField] [Required]
		private Transform _endPoint;
		[SerializeField] [Required]
		private SpinnerView _spinnerPrefab;
		[SerializeField]
		private Transform _activeSpinnerContainer;
		[SerializeField]
		private ActiveSpinnerView _activeSpinnerViewPrefab;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance<SingleNotePrefabConfig>(_singleNotePrefabConfig);
			builder.RegisterInstance<SpinnerView>(_spinnerPrefab);
			builder.Register<IElementFactory, SingleNoteViewFactory>(Lifetime.Scoped);
			builder.Register<IElementFactory, SpinnerViewFactory>(Lifetime.Scoped);
			builder.Register<ElementViewFactory>(Lifetime.Singleton);
			builder.Register<NotesVisualSystem>((resolver) =>
			{
				var viewFactory = resolver.Resolve<ElementViewFactory>();
				var mapTime = resolver.Resolve<IMapTime>();
				return new NotesVisualSystem(
					viewFactory,
					mapTime,
					_container,
					_startPoint,
					_endPoint);
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