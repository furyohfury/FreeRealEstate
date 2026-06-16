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
        private readonly PlayerScoreTable _playerScoreTable;

        public RoundInfoPresenter(SessionSystem sessionSystem, ScoreSystem scoreSystem, PlayerScoreTable playerScoreTable)
        {
            _sessionSystem = sessionSystem;
            _scoreSystem = scoreSystem;
            _playerScoreTable = playerScoreTable;
        }

        public void Initialize()
        {
            _scoreSystem.PlayerScores.OnListChanged += UpdateVisual;
            UpdateVisual();
        }

        private void UpdateVisual(NetworkListEvent<PlayerScoreData> _)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            // var currentPlayerNum = 0;
            // string[] scores =
            // {
            //     "0", "0"
            // };
            //
            // var playerDatasNetList = _sessionSystem.PlayerDatas;
            // var playerDatas = new PlayerData[playerDatasNetList.Count];
            //
            // for (int i = 0, count = playerDatasNetList.Count; i < count; i++)
            // {
            //     playerDatas[i] = playerDatasNetList[i];
            // }
            //
            // NetworkList<PlayerScoreData> playerScores = _scoreSystem.PlayerScores;
            // // Debug.Log($"playerdatas count: {playerDatas.Length}, playerscores count: {playerScores.Count}");
            //
            // foreach (PlayerData playerData in Enumerable.OrderBy(playerDatas, data => data.clientID))
            // {
            //     foreach (PlayerScoreData playerScoreData in playerScores)
            //     {
            //         if (playerData.clientID != playerScoreData.ClientId)
            //         {
            //             continue;
            //         }
            //
            //         scores[currentPlayerNum] = playerScoreData.Score.ToString();
            //         _roundInfoUI.SetPlayerName(currentPlayerNum,
            //             string.IsNullOrEmpty(playerData.Nickname.Value)
            //                 ? "Player " + currentPlayerNum
            //                 : playerData.Nickname.Value);
            //         currentPlayerNum++;
            //     }
            // }
            //
            // _roundInfoUI.SetScore(string.Concat(scores[0], " : ", scores[1]));
        }

        public void Dispose()
        {
            _scoreSystem.PlayerScores.OnListChanged -= UpdateVisual;
        }
    }
}
