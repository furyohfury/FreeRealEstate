using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Infrastructure
{
    public class BootstrapHelper : MonoBehaviour
    {
        private void Start()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;

            if (BootstrapCrossSceneData.SwitchedScene == false && currentScene != (int)Scene.Bootstrap)
            {
                BootstrapCrossSceneData.NeedToLoad = true;
                BootstrapCrossSceneData.SceneToLoad = currentScene;
                SceneManager.LoadScene((int)Scene.Bootstrap, LoadSceneMode.Additive);
            }
        }
    }
}
