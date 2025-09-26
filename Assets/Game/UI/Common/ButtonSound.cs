using Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.UI
{
	[RequireComponent(typeof(Button))]
	public sealed class ButtonSound : MonoBehaviour
	{
		[SerializeField]
		private string _clipName = "buttonClick";
		private AudioManager _audioManager;
		private Button _button;

		[Inject]
		private void Construct(AudioManager audioManager)
		{
			_audioManager = audioManager;
		}

		private void Awake()
		{
			_button = GetComponent<Button>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(OnButtonClicked);
		}

		private void OnButtonClicked()
		{
			_audioManager.PlaySoundOneShot(_clipName, AudioOutput.UI).Forget();
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnButtonClicked);
		}
	}
}