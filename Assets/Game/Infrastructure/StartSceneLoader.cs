using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Infrastructure
{
    [DefaultExecutionOrder(10000)]
    public sealed class StartSceneLoader : MonoBehaviour
    {
        [SerializeField]
        private Scene _startScene;

        private void Start()
        {
            if (BootstrapCrossSceneData.BootstrapCompleted == false)
            {
                BootstrapCrossSceneData.BootstrapCompleted = true;
                SceneSwitcher.Instance.SwitchScene(_startScene, LoadSceneMode.Single);
            }
            else
            {
                BootstrapCrossSceneData.BootstrapCompleted = true;
                SceneSwitcher.Instance.SwitchScene(BootstrapCrossSceneData.SceneToLoad, LoadSceneMode.Single);
            }
        }
    }
}
