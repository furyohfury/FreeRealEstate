using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class ButtonClick : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ClickSound);
        }

        private void ClickSound()
        {
            // TODO sfx
            AudioManager.Instance.PlayClickButtonSound();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ClickSound);
        }
    }
}
