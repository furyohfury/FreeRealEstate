using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public sealed class QuickPlayMenu : MonoBehaviour
	{
		public Observable<Unit> OnHostButtonPressed;
		public Observable<Unit> OnBackButtonPressed;

		public string GeneralMessageText
		{
			get => _generalText.text;
			set => _generalText.text = value;
		}

		[SerializeField]
		private TMP_Text _generalText;
		[SerializeField]
		private Button _hostBTN;
		[SerializeField]
		private Button _backBTN;
		[SerializeField]
		private LoadingUIAnimation _loadingUIAnimation;

		public void Init()
		{
			OnHostButtonPressed = _hostBTN.OnClickAsObservable();
			OnBackButtonPressed = _backBTN.OnClickAsObservable();
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void ShowHostButton()
		{
			_hostBTN.gameObject.SetActive(true);
		}

		public void HideHostButton()
		{
			_hostBTN.gameObject.SetActive(false);
		}

		public void ShowBackButton()
		{
			_backBTN.gameObject.SetActive(true);
		}

		public void HideBackButton()
		{
			_backBTN.gameObject.SetActive(false);
		}

		public void ShowLoadingUIAnimation()
		{
			_loadingUIAnimation.Show();
		}

		public void HideLoadingUIAnimation()
		{
			_loadingUIAnimation.Hide();
		}
		
		public void SetHostButtonInteractable(bool isActive)
		{
			_hostBTN.interactable = isActive;
		}
		
		public void SetBackButtonInteractable(bool isActive)
		{
			_backBTN.interactable = isActive;
		}
	}
}