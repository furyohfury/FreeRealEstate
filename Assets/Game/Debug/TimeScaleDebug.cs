using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class TimeScaleDebug : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private void Update()
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                Time.timeScale = Time.timeScale == 0
                    ? 1
                    : 0;
            }

            _text.text = Time.timeScale.ToString();
        }
    }
}
