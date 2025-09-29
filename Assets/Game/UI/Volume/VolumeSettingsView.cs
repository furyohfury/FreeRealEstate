using Audio;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.UI.Volume
{
	public class VolumeSettingsView : MonoBehaviour
	{
		[SerializeField]
		private Slider _musicSlider;
		[SerializeField]
		private Slider _effectsSlider;

		private AudioManager _audioManager;

		[Inject]
		public void Construct(AudioManager audioManager)
		{
			_audioManager = audioManager;
		}

		private void Start()
		{
			var musicVolume = _audioManager.GetVolume01(AudioOutput.Music);
			var uiVolume = _audioManager.GetVolume01(AudioOutput.UI);

			_musicSlider.value = musicVolume;
			_effectsSlider.value = uiVolume;
			_musicSlider.OnValueChangedAsObservable()
			            .Skip(1)
			            .Subscribe(val => _audioManager.SetVolume01(AudioOutput.Music, val))
			            .AddTo(this);

			_effectsSlider.OnValueChangedAsObservable()
			              .Skip(1)
			              .Subscribe(val => _audioManager.SetVolume01(AudioOutput.UI, val))
			              .AddTo(this);
		}
	}
}