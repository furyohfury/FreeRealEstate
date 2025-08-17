using System;
using R3;
using VContainer.Unity;

namespace Game.Visuals
{
	public sealed class ViewsRegistryAddController : IInitializable, IDisposable
	{
		private readonly ElementsVisualsSpawner _elementsVisualsSpawner;
		private readonly ElementViewsRegistry _registry;

		private IDisposable _disposable;

		public ViewsRegistryAddController(ElementsVisualsSpawner elementsVisualsSpawner, ElementViewsRegistry registry)
		{
			_elementsVisualsSpawner = elementsVisualsSpawner;
			_registry = registry;
		}

		public void Initialize()
		{
			_disposable = _elementsVisualsSpawner.OnElementViewSpawned
			                                     .Subscribe(
				                                     pair => _registry.AddPair(pair)
			                                     );
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}