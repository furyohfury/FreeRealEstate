using Audio;
using Cysharp.Threading.Tasks;
using ObjectProvide;
using UnityEngine;

namespace Game.Audio
{
	public class AddressableClipProvider : IAudioClipProvider
	{
		private readonly IObjectProvider _assetProvider;
		private readonly AudioLibrary _audioIdLibrary;

		public AddressableClipProvider(IObjectProvider assetProvider, AudioLibrary audioIdLibrary)
		{
			_assetProvider = assetProvider;
			_audioIdLibrary = audioIdLibrary;
		}

		public async UniTask<AudioClip> GetClipAsync(string clipName)
		{
			if (_audioIdLibrary.TryGetClipIdByName(clipName, out string clipId))
			{
				return await _assetProvider.Get<AudioClip>(clipId);
			}

			Debug.LogError($"Clip ID for name '{clipName}' not found in AudioLibrary.");
			return null;
		}
	}
}