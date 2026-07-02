using TMPro;
using UnityEngine;

namespace Game
{
    public sealed class PlayerNicknameUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text  _playerNicknameField;

        public void SetNickname(string nickname)
        {
            _playerNicknameField.text = nickname;
        }
    }
}
