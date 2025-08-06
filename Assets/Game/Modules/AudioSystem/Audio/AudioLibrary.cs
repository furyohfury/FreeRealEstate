using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
	[CreateAssetMenu(fileName = nameof(AudioLibrary), menuName = "Data/Audio/Library")]
	public sealed class AudioLibrary : ScriptableObject
	{
		[Serializable]
		private struct AudioClipByName
		{
			public string Name;
			public string ClipId;
		}

		[SerializeField]
		private AudioClipByName[] _clips;

		private Dictionary<string, string> _clipsDict = new();

		private void OnEnable()
		{
			_clipsDict = _clips.ToDictionary(clip => clip.Name, clip => clip.ClipId);
		}

		public bool TryGetClipIdByName(string audioClipName, out string id)
		{
			return _clipsDict.TryGetValue(audioClipName, out id);
		}
	}
}