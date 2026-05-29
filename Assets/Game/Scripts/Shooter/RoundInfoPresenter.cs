using System;
using Unity.Netcode;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;
using IInitializable = Zenject.IInitializable;

namespace Game
{
    public class RoundInfoPresenter : IInitializable, IDisposable
    {
        private readonly SessionSystem _sessionSystem;
        private readonly ScoreSystem _scoreSystem;
        private readonly RoundInfoUI _roundInfoUI;

        public RoundInfoPresenter(SessionSystem sessionSystem, ScoreSystem scoreSystem, RoundInfoUI roundInfoUI)
        {
            _sessionSystem = sessionSystem;
            _scoreSystem = scoreSystem;
            _roundInfoUI = roundInfoUI;
        }

        public void Initialize()
        {
            _roundInfoUI.SetPlayerName(0, "Player 1");
            _roundInfoUI.SetPlayerName(1, "Player 2");
            _roundInfoUI.SetScore("0 : 0");
            _scoreSystem.PlayerScores.OnListChanged += UpdateVisual;
            UpdateVisual();
        }

        private void UpdateVisual(NetworkListEvent<PlayerScoreData> _)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            var currentPlayerNum = 0;
            string[] scores =
            {
                "0", "0"
            };

            var playerDatasNetList = _sessionSystem.PlayerDatas;
            var playerDatas = new PlayerData[playerDatasNetList.Count];

            for (int i = 0, count = playerDatasNetList.Count; i < count; i++)
            {
                playerDatas[i] = playerDatasNetList[i];
            }

            NetworkList<PlayerScoreData> playerScores = _scoreSystem.PlayerScores;
            // Debug.Log($"playerdatas count: {playerDatas.Length}, playerscores count: {playerScores.Count}");

            foreach (PlayerData playerData in Enumerable.OrderBy(playerDatas, data => data.clientID))
            {
                foreach (PlayerScoreData playerScoreData in playerScores)
                {
                    if (playerData.clientID != playerScoreData.ClientId)
                    {
                        continue;
                    }

                    scores[currentPlayerNum] = playerScoreData.Score.ToString();
                    _roundInfoUI.SetPlayerName(currentPlayerNum,
                        string.IsNullOrEmpty(playerData.Nickname.Value)
                            ? "Player " + currentPlayerNum
                            : playerData.Nickname.Value);
                    currentPlayerNum++;
                }
            }

            _roundInfoUI.SetScore(string.Concat(scores[0], " : ", scores[1]));
        }

        // private void PlayerScoresOnOnListChanged(NetworkListEvent<PlayerScoreData> changeEvent)
        // {
        //     string firstScore = "";
        //     string secondScore = "";
        //
        //     foreach (var kvp in _playerNumberToDataMap)
        //     {
        //         int playerNumber = kvp.Key;
        //         PlayerData playerData = kvp.Value;
        //         PlayerScoreData playerScoreData = changeEvent.Value;
        //
        //         if (playerData.clientID == playerScoreData.ClientId)
        //         {
        //             if (playerNumber == 1)
        //             {
        //                 firstScore = playerScoreData.Score.ToString();
        //             }
        //             else if (playerNumber == 2)
        //             {
        //                 secondScore = playerScoreData.Score.ToString();
        //             }
        //         }
        //     }
        //
        //     _roundInfoUI.SetScore(firstScore + " " + secondScore);
        // }
        //
        // private void OnPlayerJoined(PlayerData obj)
        // {
        //     if (_currentPlayerNum >= 2)
        //     {
        //         return;
        //     }
        //
        //     _roundInfoUI.SetPlayerName(_currentPlayerNum, obj.Nickname);
        //     _playerNumberToDataMap[_currentPlayerNum++] = obj;
        // }

        public void Dispose()
        {
            _scoreSystem.PlayerScores.OnListChanged -= UpdateVisual;
        }
    }
}
