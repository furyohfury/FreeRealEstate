using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using Utils;

namespace Audio
{
	public sealed class AudioManager : IDisposable
	{
		private readonly AudioMixer _audioMixer;
		private readonly IAudioClipProvider _clipProvider;
		private Dictionary<AudioOutput, AudioLayer> _audioLayers;
		private float _mainChannel;
		private float _uiChannel;
		private float _musicChannel;
		private readonly AudioLocalSaver _audioLocalSaver;
		private readonly AudioLayerFactory _audioLayerFactory;
		private readonly Transform _rootAudioLayer;
		private readonly AudioLayerPool _audioLayerPool;

		public AudioManager(
			IAudioClipProvider clipProvider,
			AudioMixer audioMixer,
			AudioLibrary audioLibrary,
			AudioLayerSetting audioLayerSetting
		)
		{
			_clipProvider = clipProvider;
			_audioMixer = audioMixer;
			_audioLocalSaver = new AudioLocalSaver();
			var layerSetting = audioLayerSetting;
			_audioLayerFactory = new AudioLayerFactory(layerSetting);
			AudioMixerGroup audioMixerGroup = GetOutput(EnumUtils<AudioOutput>.ToString(AudioOutput.Master));
			_audioLayerPool = new AudioLayerPool(audioMixerGroup, layerSetting);
			_rootAudioLayer = Camera.main!.transform;
			SetVolume();
			InitializeLayers();
		}

		public void SetVolume(AudioOutput output, float value)
		{
			string channelName = output switch
			{
				AudioOutput.None => AudioManagerStaticData.VOLUME_MAIN_CHANNEL_NAME
				, AudioOutput.Master => AudioManagerStaticData.VOLUME_MAIN_CHANNEL_NAME
				, AudioOutput.UI => AudioManagerStaticData.VOLUME_UI_CHANNEL_NAME
				, AudioOutput.Music => AudioManagerStaticData.VOLUME_MUSIC_CHANNEL_NAME
				, _ => throw new ArgumentOutOfRangeException(nameof(output), output, null)
			};
			_audioLocalSaver.Save(channelName, value);
			SetVolume();
		}

		public async UniTask PlaySound(string clipName, AudioOutput output, float volumeScale = 1.0f, float startTime = 0f)
		{
			AudioClip clipToPlay = await _clipProvider.GetClipAsync(clipName);
			if (clipToPlay == null)
			{
				return;
			}

			if (!_audioLayers.TryGetValue(output, out AudioLayer layer))
			{
				return;
			}

			layer.Play(clipToPlay, volumeScale);
			layer.SetTime(startTime);
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
			audioMixerSnapshot = _audioMixer.FindSnapshot(snapshotName);
			return audioMixerSnapshot != null;
		}

		public void StopAudioOutput(AudioOutput output)
		{
			if (!_audioLayers.TryGetValue(output, out AudioLayer layer))
			{
				return;
			}

			layer.Stop();
		}

		private void SetVolume()
		{
			_mainChannel = _audioLocalSaver.GetVolume(AudioManagerStaticData.VOLUME_MAIN_CHANNEL_NAME);
			_uiChannel = _audioLocalSaver.GetVolume(AudioManagerStaticData.VOLUME_UI_CHANNEL_NAME);
			_musicChannel = _audioLocalSaver.GetVolume(AudioManagerStaticData.VOLUME_MUSIC_CHANNEL_NAME);
			ApplySoundSettings();
		}

		private void ApplySoundSettings()
		{
			_audioMixer.SetFloat(AudioManagerStaticData.VOLUME_MAIN_CHANNEL_NAME, ToChannelVolume(_mainChannel));
			_audioMixer.SetFloat(AudioManagerStaticData.VOLUME_UI_CHANNEL_NAME, ToChannelVolume(_uiChannel));
			_audioMixer.SetFloat(AudioManagerStaticData.VOLUME_MUSIC_CHANNEL_NAME, ToChannelVolume(_musicChannel));
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
			AudioMixerGroup[] outputList = _audioMixer.FindMatchingGroups(outputName);
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