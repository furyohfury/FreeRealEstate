using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Game
{
    public sealed class PlayerScoreTablePresenter : IInitializable, IDisposable
    {
        private readonly PlayerScoreTable _table;
        private readonly ScoreSystem _scoreSystem;
        private readonly PlayerScoreItemPresenterFactory _presenterFactory;

        private readonly Dictionary<PlayerData, (PlayerScoreItemPresenter presenter, PlayerScoreItem view)> _presenters = new();

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

            _scoreSystem.PlayerScores.OnListChanged += HandleScoreChanged;
        }

        private void HandleScoreChanged(NetworkListEvent<PlayerScoreData> e)
        {
            Debug.Log("[PlayerScoreTablePresenter] HandleScoreChanged");
            PlayerScoreData playerScoreData = e.Value;

            switch (e.Type)
            {
                case NetworkListEvent<PlayerScoreData>.EventType.Add:
                case NetworkListEvent<PlayerScoreData>.EventType.Insert:
                case NetworkListEvent<PlayerScoreData>.EventType.Value:
                    if (_presenters.ContainsKey(playerScoreData.PlayerData) == false)
                    {
                        CreateItem(playerScoreData);
                    }
                    break;

                case NetworkListEvent<PlayerScoreData>.EventType.Remove:
                case NetworkListEvent<PlayerScoreData>.EventType.Clear:
                    break;
            }
        }

        private void CreateItem(PlayerScoreData scoreData)
        {
            PlayerScoreItem playerScoreItem = _table.CreateScoreItem(scoreData);
            PlayerData playerData = scoreData.PlayerData;
            PlayerScoreItemPresenter presenter = _presenterFactory.Create(playerData);
            playerScoreItem.Init(presenter);
            _presenters.Add(playerData, (presenter, playerScoreItem));
        }

        public void Dispose()
        {
            foreach (var tuple in _presenters.Values)
            {
                tuple.presenter.Dispose();
            }

            if (_scoreSystem != null)
                _scoreSystem.PlayerScores.OnListChanged -= HandleScoreChanged;
        }
    }
}
