using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public sealed class WinPopup : MonoBehaviour
	{
		public Observable<Unit> OnBackButtonPressed;

		[SerializeField]
		private Button _backBTN;

		public void Init()
		{
			OnBackButtonPressed = _backBTN.OnClickAsObservable();
		}
	}
}