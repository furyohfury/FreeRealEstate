using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Utils;

namespace Audio
{
	public sealed class AudioManager : IDisposable
	{
		public static AudioManager Instance => _instance ??= new AudioManager();
		private AudioMixer AudioMixer
		{
			get
			{
				if (_audioMixer == null)
				{
					_audioMixer = Resources.Load<AudioMixer>(AudioManagerStaticData.AUDIO_MIXER_RESOURCE_NAME);
				}

				return _audioMixer;
			}
		}

		private AudioLibrary AudioLibrary
		{
			get
			{
				if (_audioLibrary == null)
				{
					_audioLibrary = Resources.Load<AudioLibrary>(AudioManagerStaticData.AUDIO_LIBRARY_RESOURCE_NAME);
				}

				return _audioLibrary;
			}
		}

		private AudioLayerSetting AudioLayerSetting
		{
			get
			{
				if (_audioLayerSetting == null)
				{
					_audioLayerSetting = Resources.Load<AudioLayerSetting>(AudioManagerStaticData.AUDIO_LAYER_SETTING_RESOURCE_NAME);
				}

				return _audioLayerSetting;
			}
		}

		private static AudioManager _instance;
		private AudioMixer _audioMixer;
		private AudioLibrary _audioLibrary;
		private AudioLayerSetting _audioLayerSetting;
		private Dictionary<AudioOutput, AudioLayer> _audioLayers;
		private float _mainChannel;
		private float _uiChannel;
		private float _musicChannel;
		private readonly AudioLocalSaver _audioLocalSaver;
		private readonly AudioLayerFactory _audioLayerFactory;
		private readonly Transform _rootAudioLayer;
		private readonly AudioLayerPool _audioLayerPool;

		private AudioManager()
		{
			_audioLocalSaver = new AudioLocalSaver();
			_audioLayerFactory = new AudioLayerFactory(AudioLayerSetting);
			AudioMixerGroup audioMixerGroup = GetOutput(EnumUtils<AudioOutput>.ToString(AudioOutput.Master));
			_audioLayerPool = new AudioLayerPool(audioMixerGroup, AudioLayerSetting);
			_rootAudioLayer = Camera.main!.transform;
			SetVolume();
			InitializeLayers();
		}

		public bool TryGetAudioClipByName(string name, out AudioClip audioClip)
		{
			if (AudioLibrary.TryGetAudioClipByName(name, out audioClip))
			{
				return true;
			}

			audioClip = default;
			return false;
		}

		public void SetVolume(AudioOutput output, float value)
		{
			string channelName = output switch
			{
				AudioOutput.None => AudioManagerStaticData.SOUNDS_MAIN_CHANNEL_NAME
				, AudioOutput.Master => AudioManagerStaticData.SOUNDS_MAIN_CHANNEL_NAME
				, AudioOutput.UI => AudioManagerStaticData.SOUNDS_UI_CHANNEL_NAME
				, AudioOutput.Music => AudioManagerStaticData.SOUNDS_MUSIC_CHANNEL_NAME
				, AudioOutput.VFX => AudioManagerStaticData.SOUNDS_VFX_CHANNEL_NAME
				, _ => throw new ArgumentOutOfRangeException(nameof(output), output, null)
			};
			_audioLocalSaver.Save(channelName, value);
			SetVolume();
		}

		public void PlaySound(AudioClip sound, AudioOutput output, float volumeScale = 1.0f)
		{
			if (!_audioLayers.TryGetValue(output, out AudioLayer layer))
			{
				return;
			}

			layer.Play(sound, volumeScale);
		}

		public void PlaySoundOneShot(AudioClip sound, AudioOutput output, float volumeScale = 1.0f, float pitch = 1.0f)
		{
			if (!_audioLayers.TryGetValue(output, out AudioLayer layer))
			{
				return;
			}

			layer.PlayOneShot(sound, volumeScale, pitch);
		}

		public void PlaySound(AudioClip sound, Vector3 position, float volumeScale = 1.0f)
		{
			AudioLayer layer = _audioLayerPool.Get(position);
			layer.Play(sound, volumeScale);
		}

		public void PlaySoundOneShot(AudioClip sound, Vector3 position, float volumeScale = 1.0f, float pitch = 1.0f)
		{
			AudioLayer layer = _audioLayerPool.Get(position);
			layer.PlayOneShot(sound, volumeScale, pitch);
		}

		public void Transition(AudioMixerSnapshot snapshot, float crossFadeTime = AudioManagerStaticData.TRANSITION_DEFAULT_TIME)
		{
			snapshot.TransitionTo(crossFadeTime);
		}

		public bool TryGetSnapshot(string snapshotName, out AudioMixerSnapshot audioMixerSnapshot)
		{
			audioMixerSnapshot = AudioMixer.FindSnapshot(snapshotName);
			return audioMixerSnapshot != null;
		}

		private void SetVolume()
		{
			_mainChannel = _audioLocalSaver.GetVolume(AudioManagerStaticData.SOUNDS_MAIN_CHANNEL_NAME);
			_uiChannel = _audioLocalSaver.GetVolume(AudioManagerStaticData.SOUNDS_UI_CHANNEL_NAME);
			_musicChannel = _audioLocalSaver.GetVolume(AudioManagerStaticData.SOUNDS_MUSIC_CHANNEL_NAME);
			ApplySoundSettings();
		}

		private void ApplySoundSettings()
		{
			AudioMixer.SetFloat(AudioManagerStaticData.SOUNDS_MAIN_CHANNEL_NAME, ToChannelVolume(_mainChannel));
			AudioMixer.SetFloat(AudioManagerStaticData.SOUNDS_UI_CHANNEL_NAME, ToChannelVolume(_uiChannel));
			AudioMixer.SetFloat(AudioManagerStaticData.SOUNDS_MUSIC_CHANNEL_NAME, ToChannelVolume(_musicChannel));
		}

		private void InitializeLayers()
		{
			_audioLayers = new Dictionary<AudioOutput, AudioLayer>();
			AudioOutput[] audioLayers = EnumUtils<AudioOutput>.Values;
			for (var index = 0; index < audioLayers.Length; index++)
			{
				AudioOutput audioLayer = audioLayers[index];
				if (audioLayer == AudioOutput.None)
				{
					continue;
				}

				AudioMixerGroup audioMixerGroup = GetOutput(EnumUtils<AudioOutput>.ToString(audioLayer));
				AudioLayer layer = _audioLayerFactory.CreateAudioLayer(audioLayer, audioMixerGroup, _rootAudioLayer);
				_audioLayers.Add(audioLayer, layer);
			}
		}

		private AudioMixerGroup GetOutput(string outputName)
		{
			AudioMixerGroup[] outputList = AudioMixer.FindMatchingGroups(outputName);
			if (outputList == null)
			{
				Debug.LogError($"There is no output {outputName}");
				return null;
			}

			return outputList[0];
		}

		private static float ToChannelVolume(float value)
		{
			return Mathf.Clamp(value,
				AudioManagerStaticData.CHANNEL_VOLUME_MINIMUM,
				AudioManagerStaticData.CHANNEL_VOLUME_MAXIMUM);
		}

		public void Dispose()
		{
			foreach ((AudioOutput _, AudioLayer value) in _audioLayers)
			{
				value.Dispose();
			}

			_audioLayers.Clear();
		}
	}
}