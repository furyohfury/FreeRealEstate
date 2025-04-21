using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public abstract class CellViewList : MonoBehaviour
	{
		public abstract void SetCellViews(IList<CellView> cellViews, int rows, int columns);
		public abstract void DestroyViews();
		public abstract void EnableViews();
		public abstract void DisableCells();
	}
}