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

        public void TurnTo(Vector3 targetPosition)
        {
            Vector3 direction = transform.position - targetPosition;
            direction.y = 0;

            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
