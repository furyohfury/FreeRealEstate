using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Infrastructure
{
    public sealed class StartSceneLoader : MonoBehaviour
    {
        [SerializeField]
        private Scene _startScene;

        private void Start()
        {
            if (BootstrapCrossSceneData.NeedToLoad == false)
            {
                SceneManager.LoadScene((int)_startScene, LoadSceneMode.Single);
            }
        }
    }
}
