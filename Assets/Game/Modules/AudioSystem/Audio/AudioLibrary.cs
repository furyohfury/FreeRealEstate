using System;
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
            public AudioClip Clip;
        }
        
        [SerializeField] private AudioClipByName[] _clips;

        public bool TryGetAudioClipByName(string audioClipName, out AudioClip audioClip)
        {
            for (var index = 0; index < _clips.Length; index++)
            {
                AudioClipByName item = _clips[index];
                if (item.Name.Equals(audioClipName))
                {
                    audioClip = item.Clip;
                    return true;
                }
            }

            audioClip = default;
            return false;
        }
    }
}
