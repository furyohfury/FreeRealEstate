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
                Debug.LogError($"Dublicate singleton instance of {this.GetType().FullName}");
                Destroy(Instance.gameObject);
            }
            Instance = this as T;
        }
    }
}
