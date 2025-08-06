using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Audio
{
	public interface IAudioClipProvider
	{
		UniTask<AudioClip> GetClipAsync(string clipName);
	}
}