using UnityEngine;

namespace Game
{
    public sealed class DontDestroyOnLoadComponent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
