using System;
using System.Collections.Generic;
using Unity.Netcode;
using Zenject;

namespace Game
{
    public sealed class PlayerScoreTablePresenter : IInitializable, IDisposable
    {
        private readonly PlayerScoreTable _table;
        private readonly ScoreSystem _scoreSystem;
        private readonly PlayerScoreItemPresenterFactory _presenterFactory;

        private readonly Dictionary<PlayerData, PlayerScoreItemPresenter> _presenters = new Dictionary<PlayerData, PlayerScoreItemPresenter>();

        public PlayerScoreTablePresenter(PlayerScoreTable table, ScoreSystem scoreSystem, PlayerScoreItemPresenterFactory presenterFactory)
        {
            _table = table;
            _scoreSystem = scoreSystem;
            _presenterFactory = presenterFactory;
        }

        public void Initialize()
        {
            foreach (PlayerScoreData scoreData in _scoreSystem.PlayerScores)
            {
                CreateItem(scoreData);
            }

            RefreshAll();
            _scoreSystem.PlayerScores.OnListChanged += HandleScoreChanged;
        }

        private void HandleScoreChanged(NetworkListEvent<PlayerScoreData> e)
        {
            PlayerScoreData playerScoreData = e.Value;

            switch (e.Type)
            {
                case NetworkListEvent<PlayerScoreData>.EventType.Add:
                case NetworkListEvent<PlayerScoreData>.EventType.Insert:
                    CreateItem(playerScoreData);
                    break;

                case NetworkListEvent<PlayerScoreData>.EventType.Remove:
                case NetworkListEvent<PlayerScoreData>.EventType.Clear:
                    break;
            }

            RefreshAll();
        }

        private void CreateItem(PlayerScoreData scoreData)
        {
            PlayerScoreItem playerScoreItem = _table.CreateScoreItem(scoreData);
            PlayerData playerData = scoreData.PlayerData;
            var presenter = _presenterFactory.Create(playerData);
            playerScoreItem.Init(presenter);
            _presenters.Add(playerData, presenter);
        }

        private void RefreshAll()
        {
            // var viewData = new PlayerViewData[_scoreSystem.PlayerScores.Count];
            //
            // for (int i = 0; i < _scoreSystem.PlayerScores.Count; i++)
            // {
            //     PlayerScoreData scoreData = _scoreSystem.PlayerScores[i];
            //
            //     PlayerScoreItem item = _items[scoreData.ClientId];
            //     item.SetScore(scoreData.Score.ToString());
            //
            //     viewData[i] = new PlayerViewData
            //                   {
            //                       PlayerId = (int)scoreData.ClientId,
            //                       Order = i,
            //                       Score = scoreData.Score
            //                   };
            // }
            //
            // Array.Sort(viewData,
            //     (a, b) =>
            //     {
            //         int result = b.Score.CompareTo(a.Score);
            //
            //         if (result == 0)
            //             result = a.PlayerId.CompareTo(b.PlayerId);
            //
            //         return result;
            //     });
            //
            // for (int i = 0; i < viewData.Length; i++)
            // {
            //     viewData[i].Order = i;
            // }
            //
            // _table.SortItems(viewData);
        }

        public void Dispose()
        {
            foreach (PlayerScoreItemPresenter playerScoreItemPresenter in _presenters.Values)
            {
                playerScoreItemPresenter.Dispose();
            }

            if (_scoreSystem != null)
                _scoreSystem.PlayerScores.OnListChanged -= HandleScoreChanged;
        }
    }
}
