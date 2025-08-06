using Audio;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace GameDebug
{
	public class AudioDebug : MonoBehaviour
	{
		[SerializeField]
		private string _clipName;
		[Inject]
		private AudioManager _audioManager;

		[Button]
		private void PlayClip()
		{
			_audioManager.PlaySound(_clipName, AudioOutput.Music).Forget();
		}
	}
}