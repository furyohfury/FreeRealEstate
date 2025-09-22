using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public sealed class MessageWindow : MonoBehaviour, IMessageWindow
	{
		[SerializeField]
		private TMP_Text _tmpField;
		[SerializeField]
		private Button _closeButton;

		public void SetMessageText(string text)
		{
			_tmpField.SetText(text);
		}

		public void Close()
		{
			Destroy(gameObject);
		}

		private void OnEnable()
		{
			_closeButton.onClick.AddListener(OnCloseButton);
		}

		private void OnCloseButton()
		{
			Close();
		}

		private void OnDisable()
		{
			_closeButton.onClick.RemoveListener(OnCloseButton);
		}
	}
}