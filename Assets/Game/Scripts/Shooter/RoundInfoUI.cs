using TMPro;
using UnityEngine;

namespace Game
{
    public sealed class RoundInfoUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _firstPlayerName;
        [SerializeField]
        private TextMeshProUGUI _secondPlayerName;
        [SerializeField]
        private TextMeshProUGUI _score;

        public void SetScore(string score)
        {
            _score.text = score;
        }

        public void SetPlayerName(int playerNumber, string nickname)
        {
            if (playerNumber == 0)
            {
                _firstPlayerName.text = nickname;
            }
            else if (playerNumber == 1)
            {
                _secondPlayerName.text = nickname;
            }
            else
            {
                Debug.LogError($"Player {playerNumber} has no player name");
            }
        }
    }
}
