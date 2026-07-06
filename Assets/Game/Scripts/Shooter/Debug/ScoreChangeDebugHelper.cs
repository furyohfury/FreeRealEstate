using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Scripts.Shooter
{
    public sealed class ScoreChangeDebugHelper : MonoBehaviour
    {
        [Inject]
        private ScoreSystem _scoreSystem;
        [SerializeField]
        private KeyCode _setScoreKey = KeyCode.L;

        // private void Start()
        // {
        //     _scoreSystem.PlayerScores.OnListChanged += PlayerScoresOnOnListChanged;
        // }
        //
        // private void PlayerScoresOnOnListChanged(NetworkListEvent<PlayerScoreData> changeEvent)
        // {
        //     Debug.Log("Player scores changed");
        // }
        //
        // private void Update()
        // {
        //     var inputControl = Keyboard.current[_setScoreKey.ToString().ToLower()];
        //     if (inputControl.IsPressed())
        //     {
        //         _scoreSystem.SetScore(0, 10);
        //     }
        // }
        //
        // private void OnDestroy()
        // {
        //     _scoreSystem.PlayerScores.OnListChanged -= PlayerScoresOnOnListChanged;
        // }
    }
}
