using UnityEngine;

namespace Game
{
	public sealed class CellList : ScriptableObject
	{
		public Cell[] Cells => _cells;
		
		[SerializeField]
		private Cell[] _cells;
	}
}