using UnityEngine;

namespace Game
{
	[CreateAssetMenu(fileName = "CellList", menuName = "Create config/CellList")]
	public sealed class CellBundle : ScriptableObject
	{
		public Cell[] Cells => _cells;
		public int Rows => _rows;
		public int Columns => _columns;

		[SerializeField]
		private Cell[] _cells;

		[SerializeField]
		private int _rows;

		[SerializeField]
		private int _columns;
	}
}