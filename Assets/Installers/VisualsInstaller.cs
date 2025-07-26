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

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance<SingleNotePrefabConfig>(_singleNotePrefabConfig);
			builder.Register<IElementFactory, SingleNoteViewFactory>(Lifetime.Scoped);
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
			}, Lifetime.Scoped);

			Debug.Log("Successfully installed all visual systems");
		}
	}
}