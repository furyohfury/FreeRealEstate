using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class DifficultySelectionUI : MonoBehaviour
    {
        [SerializeField]
        private SessionParamsStorage _sessionParamsStorage;
        [SerializeField]
        private Transform _container;
        [SerializeField]
        private ButtonUI _buttonPrefab;
        private readonly Dictionary<ButtonUI, int> _buttonToParamsMap = new Dictionary<ButtonUI, int>();

        private void Start()
        {
            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }
            
            SessionParams[] sessionParams = _sessionParamsStorage.SessionParams;

            for (int i = 0, count = sessionParams.Length; i < count; i++)
            {
                ButtonUI button = Instantiate(_buttonPrefab, _container);
                button.SetText(sessionParams[i].Id);
                button.OnClick += OnButtonClicked;
                _buttonToParamsMap.Add(button, i);
            }
        }

        private void OnButtonClicked(ButtonUI buttonUI)
        {
            buttonUI.OnClick -= OnButtonClicked;
            int paramsIndex = _buttonToParamsMap[buttonUI];
            GameParamsService.Instance.SessionParams = _sessionParamsStorage.SessionParams[paramsIndex];
            SceneManager.LoadScene(Scene.Gameplay.ToString(), LoadSceneMode.Single);
        }
    }
}
