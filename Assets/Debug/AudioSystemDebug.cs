using Audio;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
	public sealed class AudioSystemDebug : MonoBehaviour
	{
		[SerializeField]
		private AudioClip _clip;
		[SerializeField]
		private AudioClip _music;
		[SerializeField]
		private Transform _point;

		[Button]
		private void PlayCachedSound()
		{
			AudioManager.Instance.PlaySoundOneShot(_clip, AudioOutput.Master, 0.6f);
		}
		
		[Button]
		private void PlayCachedSoundAtPos()
		{
			AudioManager.Instance.PlaySoundOneShot(_clip, _point.position);
		}

		[Button]
		private void PlaySound(string s)
		{
			if (AudioManager.Instance.TryGetAudioClipByName(s, out var clip))
			{
				AudioManager.Instance.PlaySoundOneShot(clip, AudioOutput.Master, 0.6f);
			}
			else
			{
				Debug.Log("Didnt find sound");
			}
		}

		[Button]
		private void PlaySoundInUI(string s)
		{
			if (AudioManager.Instance.TryGetAudioClipByName(s, out var clip))
			{
				AudioManager.Instance.PlaySoundOneShot(clip, AudioOutput.UI, 0.6f);
			}
			else
			{
				Debug.Log("Didnt find sound");
			}
		}

		[Button]
		private void PlaySoundInMusic()
		{
			AudioManager.Instance.PlaySound(_music, AudioOutput.Music, 0.6f);
		}
		
		[Button]
		private void PlayTwoAtOnce()
		{
			AudioManager.Instance.PlaySound(_music, AudioOutput.Master, 0.6f);
			if (AudioManager.Instance.TryGetAudioClipByName("Laser", out var clip))
			{
				AudioManager.Instance.PlaySound(clip, AudioOutput.Master, 0.6f);
			}
		}
	}
}