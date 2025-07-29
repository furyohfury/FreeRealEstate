using UnityEngine;

namespace Game.Visuals
{
	public sealed class ActiveSpinnerFactory
	{
		private readonly ActiveSpinnerView _prefab;

		public ActiveSpinnerFactory(ActiveSpinnerView prefab)
		{
			_prefab = prefab;
		}

		public ActiveSpinnerView Spawn(Transform container)
		{
			return Object.Instantiate(_prefab, container);
		}
	}
}