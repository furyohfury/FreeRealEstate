using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
	public sealed class CellGridView : CellViewList
	{
		[SerializeField]
		private List<CellView> _cellViews = new();
		[SerializeField]
		private float _spacingX;
		[SerializeField]
		private float _spacingY;

		public override void SetCellViews(IList<CellView> cellViews, int rows, int columns)
		{
			_cellViews.Clear();
			_cellViews.AddRange(cellViews);

			for (int y = 0, ind = 0; y < rows; y++)
			{
				for (int x = 0; x < columns; x++)
				{
					if (ind >= cellViews.Count)
					{
						break;
					}

					Vector2 position = new Vector2(
						transform.position.x + x * _spacingX,
						transform.position.y - y * _spacingY
					);

					var cellViewTransform = cellViews[ind].transform;
					cellViewTransform.position = position;
					cellViewTransform.SetParent(transform);
					ind++;
				}
			}
		}

		public override void DestroyViews()
		{
			foreach (var cellView in _cellViews.ToList())
			{
				_cellViews.Remove(cellView);
				Destroy(cellView.gameObject);
			}
		}

		public override void EnableViews()
		{
			foreach (var cellView in _cellViews)
			{
				cellView.Enable();
			}
		}

		public override void DisableCells()
		{
			foreach (var cellView in _cellViews)
			{
				cellView.Disable();
			}
		}
	}
}