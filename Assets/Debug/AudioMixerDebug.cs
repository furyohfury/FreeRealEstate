using System;
using Audio;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;

namespace GameDebug
{
	public class AudioMixerDebug : MonoBehaviour
	{
		[Inject]
		private AudioMixer _audioMixer;
		[Inject]
		private AudioManager _audioManager;

		private void Start()
		{
			// _audioManager.SetSavedVolume();
		}

		[Button]
		private void SetVolume(float volume)
		{
			_audioMixer.SetFloat(AudioManagerStaticData.VOLUME_MUSIC_CHANNEL_NAME, volume);
		}
	}
}