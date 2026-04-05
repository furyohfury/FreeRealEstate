using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        public void SetRatio01(float ratio)
        {
            ratio = Mathf.Clamp01(ratio);
            _image.fillAmount = ratio;
        }
    }
}
