using UnityEngine;

namespace Game
{
	public sealed class CellViewFactory
	{
		private readonly CellView _prefab;

		public CellViewFactory(CellView prefab)
		{
			_prefab = prefab;
		}

		public CellView CreateCellView(Vector3 pos, Quaternion rot)
		{
			return Object.Instantiate(_prefab, pos, rot);
		}
		
		public CellView CreateCellView()
		{
			return Object.Instantiate(_prefab);
		}
	}
}