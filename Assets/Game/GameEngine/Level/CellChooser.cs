using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
	public sealed class CellChooser
	{
		public Subject<bool> OnGuessed = new();

		[ShowInInspector]
		private Cell _desiredCell;

		public void SetDesiredCell(Cell cell)
		{
			_desiredCell = cell;
#if UNITY_EDITOR
			Debug.Log("Desired cell is now " + cell.ID);
#endif
		}

		public bool ChooseCell(Cell cell)
		{
			bool guessed = cell == _desiredCell;
			OnGuessed.OnNext(guessed);

			return guessed;
		}
	}
}