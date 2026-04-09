using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class DayNightButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private LightChanger _lightChanger;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _lightChanger.SwitchToOpposite();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
    }
}
