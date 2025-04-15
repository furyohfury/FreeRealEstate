using UnityEngine;

namespace Game
{
	[CreateAssetMenu(fileName = "CellList", menuName = "Create config/CellList")]
	public sealed class CellList : ScriptableObject
	{
		public Cell[] Cells => _cells;
		
		[SerializeField]
		private Cell[] _cells;
	}
}