using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Infrastructure
{
    [DefaultExecutionOrder(-1000)]
    public class NonBootstrapSceneInitializer : MonoBehaviour
    {
        private void Start()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;

            if (BootstrapCrossSceneData.BootstrapCompleted == false && currentScene != (int)Scene.Bootstrap)
            {
                BootstrapCrossSceneData.BootstrapCompleted = true;
                BootstrapCrossSceneData.SceneToLoad = currentScene;
                SceneManager.LoadScene((int)Scene.Bootstrap);
            }
        }
    }
}
