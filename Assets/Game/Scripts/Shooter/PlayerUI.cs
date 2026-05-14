using TMPro;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class PlayerUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _hpText;

        public void SetText(string text)
        {
            _hpText.text = text;
        }
    }
}
