using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Infrastructure
{
    [DefaultExecutionOrder(5000)]
    public sealed class BootstrapEditorSceneLoader : MonoBehaviour
    {
        private void Start()
        {
            if (BootstrapCrossSceneData.NeedToLoad)
            {
                BootstrapCrossSceneData.NeedToLoad = false;
                BootstrapCrossSceneData.SwitchedScene = true;
                SceneManager.UnloadSceneAsync((int)Scene.Bootstrap);
            }
        }
    }
}
