using Game.Infrastructure;
using UnityEngine;

namespace Game
{
    public sealed class LoadingScreen : MonoBehaviour
    {
        [SerializeField]
        private ProgressBar _progressBar;

        private void Start()
        {
            SceneSwitcher.Instance.OnProgressChanged += OnProgressChanged;
        }

        private void OnProgressChanged(float obj)
        {
            _progressBar.SetRatio01(obj);
        }

        private void OnDestroy()
        {
            SceneSwitcher.Instance.OnProgressChanged -= OnProgressChanged;
        }
    }
}
