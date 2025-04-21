using R3;
using UnityEngine;

namespace Game
{
	public abstract class CellView : MonoBehaviour
	{
		public Subject<Unit> OnClicked = new();

		public abstract void SetSprite(Sprite sprite);

		public abstract void DoWrongChoiceEffect();

		public abstract void DoCorrectChoiceEffect();
	}
}