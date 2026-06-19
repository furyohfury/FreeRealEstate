using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class PlayerScoreTable : MonoBehaviour
    {
        public Transform Container => _container;

        [Header("Parameters")]
        [SerializeField]
        private float _sortAnimationDuration = 0.5f;
        [SerializeField]
        private Ease _sortAnimationEase = Ease.Linear;
        [Header("References")]
        [SerializeField]
        private Transform _container;
        [SerializeField]
        private PlayerScoreItem _playerScoreItemPrefab;
        [SerializeField]
        private VerticalLayoutGroup _verticalLayoutGroup;

        private List<PlayerIdScoreItemPair> _scoreItemsMap = new List<PlayerIdScoreItemPair>();

        private void Awake()
        {
            _verticalLayoutGroup.enabled = false;
            
            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }
        }

        public void AddScore(int id)
        {
            _verticalLayoutGroup.enabled = true;
            var scoreItem = Instantiate(_playerScoreItemPrefab, _container);
            _scoreItemsMap.Add(new PlayerIdScoreItemPair(id, scoreItem));
            _verticalLayoutGroup.enabled = false;
        }

        public void RemoveScore(int id)
        {
            _verticalLayoutGroup.enabled = true;
            
            for (int i = 0, count = _scoreItemsMap.Count; i < count; i++)
            {
                if (_scoreItemsMap[i].PlayerId == id)
                {
                    _scoreItemsMap.RemoveAt(i);
                }
            }
            
            _verticalLayoutGroup.enabled = false;
        }

        public void SortItems(PlayerViewData[] playerViewData)
        {
            Array.Sort(playerViewData, (data, other) => data.Order.CompareTo(other.Order));

            var newPairs = new List<PlayerIdScoreItemPair>(_scoreItemsMap.Count);

            for (int i = 0, count = playerViewData.Length; i < count; i++)
            {
                int playerId = playerViewData[i].PlayerId;
                int order = playerViewData[i].Order;
                var playerScoreItem = _scoreItemsMap.Find(pair => pair.PlayerId == playerId).ScoreItem;
                PlayerScoreItem otherScoreItem = _scoreItemsMap[order].ScoreItem;
                playerScoreItem.Move(otherScoreItem.GetPosition(), _sortAnimationDuration, _sortAnimationEase);
                // newPairs[i] = new PlayerIdScoreItemPair(playerId, playerScoreItem);
            }

            _scoreItemsMap = newPairs;
        }

        private struct PlayerIdScoreItemPair
        {
            public readonly int PlayerId;
            public readonly PlayerScoreItem ScoreItem;

            public PlayerIdScoreItemPair(int playerId, PlayerScoreItem scoreItem)
            {
                PlayerId = playerId;
                ScoreItem = scoreItem;
            }
        }
    }
}
