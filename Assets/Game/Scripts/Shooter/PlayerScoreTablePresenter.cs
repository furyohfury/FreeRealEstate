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
        private readonly SessionSystem _sessionSystem;

        private readonly Dictionary<ulong, PlayerScoreItem> _items = new Dictionary<ulong, PlayerScoreItem>();
        private LobbySystem _lobbySystem;

        [Inject]
        public PlayerScoreTablePresenter(PlayerScoreTable table, ScoreSystem scoreSystem, SessionSystem sessionSystem, LobbySystem lobbySystem)
        {
            _lobbySystem = lobbySystem;
            _table = table;
            _scoreSystem = scoreSystem;
            _sessionSystem = sessionSystem;
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
            switch (e.Type)
            {
                case NetworkListEvent<PlayerScoreData>.EventType.Add:
                case NetworkListEvent<PlayerScoreData>.EventType.Insert:
                    CreateItem(e.Value);
                    break;

                case NetworkListEvent<PlayerScoreData>.EventType.Remove:
                    _table.RemoveScore((int)e.Value.ClientId);
                    _items.Remove(e.Value.ClientId);
                    break;

                case NetworkListEvent<PlayerScoreData>.EventType.Clear:
                    foreach (ulong id in _items.Keys)
                    {
                        _table.RemoveScore((int)id);
                    }

                    _items.Clear();
                    break;
            }

            RefreshAll();
        }

        private void CreateItem(PlayerScoreData scoreData)
        {
            if (_items.ContainsKey(scoreData.ClientId))
                return;

            // PlayerData player = _sessionSystem.PlayerDatas.first(scoreData.ClientId);
            // PlayerScoreItem item = _table.AddScore((int)scoreData.ClientId);
            // item.SetPlayerName(player.Nickname.ToString());
            // item.SetScore(scoreData.Score.ToString());

            // если есть аватар
            // item.SetAvatar(...);

            // _items.Add(scoreData.ClientId, item);
        }

        private void RefreshAll()
        {
            var viewData = new PlayerViewData[_scoreSystem.PlayerScores.Count];

            for (int i = 0; i < _scoreSystem.PlayerScores.Count; i++)
            {
                PlayerScoreData scoreData = _scoreSystem.PlayerScores[i];

                PlayerScoreItem item = _items[scoreData.ClientId];
                item.SetScore(scoreData.Score.ToString());

                viewData[i] = new PlayerViewData
                              {
                                  PlayerId = (int)scoreData.ClientId
                                  , Order = i
                                  , Score = scoreData.Score
                              };
            }

            Array.Sort(viewData, (a, b) =>
            {
                int result = b.Score.CompareTo(a.Score);

                if (result == 0)
                    result = a.PlayerId.CompareTo(b.PlayerId);

                return result;
            });

            for (int i = 0; i < viewData.Length; i++)
            {
                viewData[i].Order = i;
            }

            _table.SortItems(viewData);
        }

        public void Dispose()
        {
            if (_scoreSystem != null)
                _scoreSystem.PlayerScores.OnListChanged -= HandleScoreChanged;
        }
    }
}
