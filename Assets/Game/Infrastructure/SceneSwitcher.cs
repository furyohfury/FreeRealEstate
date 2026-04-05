using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Infrastructure
{
    public sealed class SceneSwitcher : Singleton<SceneSwitcher>
    {
        public event Action<float> OnProgressChanged;

        [SerializeField]
        private float _minimalLoadingTime = 1f;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        public void SwitchScene(Scene scene, LoadSceneMode mode)
        {
            SceneManager.LoadSceneAsync((int)scene, mode);
        }
        
        public void SwitchScene(int scene, LoadSceneMode mode)
        {
            SceneManager.LoadSceneAsync(scene, mode);
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void SwitchSceneWithLoadingScreen(Scene scene)
        {
            StartCoroutine(LoadSceneAsync(scene));
        }

        private IEnumerator LoadSceneAsync(Scene scene)
        {
            var activeScene = SceneManager.GetActiveScene();

            yield return SceneManager.LoadSceneAsync((int)Scene.LoadingScreen, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)Scene.LoadingScreen));

            yield return SceneManager.UnloadSceneAsync(activeScene);

            var op = SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);

            if (op == null)
            {
                Debug.LogError($"{nameof(LoadSceneAsync)} returned null");
                yield break;
            }

            op.allowSceneActivation = false;
            float timer = 0f;

            while (op.progress < 0.9f)
            {
                OnProgressChanged?.Invoke(op.progress);
                timer += Time.deltaTime;
                yield return null;
            }
            
            OnProgressChanged?.Invoke(op.progress);

            while (timer <= _minimalLoadingTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            op.allowSceneActivation = true;

            while (!op.isDone)
            {
                yield return null;
            }

            yield return SceneManager.UnloadSceneAsync((int)Scene.LoadingScreen);

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)scene));
        }
    }
}
