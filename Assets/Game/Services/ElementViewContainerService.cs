using UnityEngine;

namespace Game.Services
{
	public sealed class ElementViewContainerService
	{
		public Transform Container => _container;
		private readonly Transform _container;

		public ElementViewContainerService(Transform container)
		{
			_container = container;
		}
	}
}