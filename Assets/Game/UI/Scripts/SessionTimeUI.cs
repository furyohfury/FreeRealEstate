using System;
using TMPro;
using UnityEngine;

namespace Game
{
    public sealed class SessionTimeUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private void Update()
        {
            float currentTime = GameLoop.Instance.CurrentTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            _text.text = timeSpan.ToString(@"mm\:ss");
        }
    }
}
