using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
	public sealed class CellChooser
	{
		public Subject<bool> OnGuessed = new();
		public ReadOnlyReactiveProperty<Cell> DesiredCell => _desiredCell;

		[ShowInInspector]
		private ReactiveProperty<Cell> _desiredCell = new();

		public void SetDesiredCell(Cell cell)
		{
			_desiredCell.Value = cell;
#if UNITY_EDITOR
			Debug.Log("Desired cell is now " + cell.ID);
#endif
		}

		public bool ChooseCell(Cell cell)
		{
			bool guessed = cell == _desiredCell.Value;
			OnGuessed.OnNext(guessed);

			return guessed;
		}
	}
}