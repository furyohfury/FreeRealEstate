using UnityEngine;

namespace Game
{
    public sealed class AudioPool : MonoBehaviour
    {
        private int _counter;

        public AudioSource GetSource()
        {
            var go = new GameObject("AudioSource_" + _counter++);
            go.transform.SetParent(transform);
            
            return go.AddComponent<AudioSource>();
        }

        public void Return(AudioSource source)
        {
            Destroy(source.gameObject);
        }
    }
}
