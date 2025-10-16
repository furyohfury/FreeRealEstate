using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public sealed class EnterNameView : MonoBehaviour
	{
		public Observable<string> OnNicknameNameEntered;
		
		[SerializeField]
		private TMP_InputField _inputField;
		[SerializeField]
		private Button _enterButton;

		private CompositeDisposable _disposable = new CompositeDisposable();

		public void Init()
		{
			_inputField.OnValueChangedAsObservable()
			           .Subscribe(value => _enterButton.interactable = string.IsNullOrEmpty(value) == false)
			           .AddTo(_disposable);

			OnNicknameNameEntered = _enterButton.OnClickAsObservable()
			                                    .Select(_ => _inputField.text);
		}

		private void OnDestroy()
		{
			_disposable.Dispose();
		}
	}
}