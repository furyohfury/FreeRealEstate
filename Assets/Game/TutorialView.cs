using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class TutorialView : MonoBehaviour
    {
        [SerializeField]
        private Button _backButton;
        [SerializeField]
        private Button _howToPlayButton;
        [SerializeField]
        private GameObject _content;

        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonPressed);
            _howToPlayButton.onClick.AddListener(OnHowToPlayButtonPressed);
        }

        private void OnHowToPlayButtonPressed()
        {
            _content.SetActive(true);
            _howToPlayButton.gameObject.SetActive(false);
        }

        private void OnBackButtonPressed()
        {
            _content.SetActive(false);
            _howToPlayButton.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonPressed);
            _howToPlayButton.onClick.RemoveListener(OnHowToPlayButtonPressed);
        }
    }
}
