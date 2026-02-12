using UnityEngine;

namespace Game
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }
            Instance = this as T;
        }
    }
}
