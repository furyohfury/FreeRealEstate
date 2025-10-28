using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public sealed class GameModeView : MonoBehaviour
	{
		public Observable<Unit> OnQuickPlayPressed;
		public Observable<Unit> OnHostGamePressed;
		public Observable<Unit> OnJoinGamePressed;
		public Observable<Unit> OnCopySessionIdPressed;
		
		public string HostSessionId
		{
			get => _hostSessionIdField.text;
			set => _hostSessionIdField.text = value;
		}

		public string JoinSessionId
		{
			get => _joinInputField.text;
			set => _joinInputField.text = value;
		}

		[SerializeField]
		private Button _quickPlayBtn;
		[Header("Host game")]
		[SerializeField]
		private Button _hostGameBtn;
		[SerializeField]
		private TMP_InputField _hostSessionIdField;
		[SerializeField]
		private Button _copySessionIdBtn;
		[Header("Join game")]
		[SerializeField]
		private Button _joinGameBtn;
		[SerializeField]
		private TMP_InputField _joinInputField;

		private string _quickPlayButtonDefaultText;

		public void Init()
		{
			OnQuickPlayPressed = _quickPlayBtn.OnClickAsObservable();
			OnHostGamePressed = _hostGameBtn.OnClickAsObservable();
			OnJoinGamePressed = _joinGameBtn.OnClickAsObservable();
			OnCopySessionIdPressed = _copySessionIdBtn.OnClickAsObservable();
		}
		
		public void SetQuickGameButtonInteractable(bool isActive)
		{
			_quickPlayBtn.interactable = isActive;
		}

		public void SetHostButtonInteractable(bool isActive)
		{
			_hostGameBtn.interactable = isActive;
		}

		public void SetJoinButtonInteractable(bool isActive)
		{
			_joinGameBtn.interactable = isActive;
		}
		
		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}