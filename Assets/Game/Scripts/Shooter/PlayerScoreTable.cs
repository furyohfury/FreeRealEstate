using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class PlayerScoreTable : MonoBehaviour
    {
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

        private Dictionary<PlayerData, PlayerScoreItem> _scoreItemsMap = new Dictionary<PlayerData, PlayerScoreItem>();
        private Sequence _activeMoveTween;

        private void Awake()
        {
            _verticalLayoutGroup.enabled = false;

            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }
        }

        public PlayerScoreItem CreateScoreItem(PlayerScoreData scoreData)
        {
            _verticalLayoutGroup.enabled = true;
            PlayerScoreItem scoreItem = Instantiate(_playerScoreItemPrefab, _container);
            _scoreItemsMap.Add(scoreData.PlayerData, scoreItem);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_verticalLayoutGroup.transform);
            _verticalLayoutGroup.enabled = false;

            return scoreItem;
        }

        public void SortItems(PlayerViewData[] playerViewData)
        {
            // _activeMoveTween?.Complete();
            // Array.Sort(playerViewData, (data, other) => data.Order.CompareTo(other.Order));
            //
            // var newPairs = new List<PlayerIdScoreItemPair>(_scoreItemsMap.Count);
            // _activeMoveTween = DOTween.Sequence();
            //
            // for (int i = 0, count = playerViewData.Length; i < count; i++)
            // {
            //     int playerId = playerViewData[i].PlayerId;
            //     int order = playerViewData[i].Order;
            //     var playerScoreItem = _scoreItemsMap.Find(pair => pair.PlayerId == playerId).ScoreItem;
            //     PlayerScoreItem otherScoreItem = _scoreItemsMap[order].ScoreItem;
            //     _activeMoveTween.Join(playerScoreItem.Move(otherScoreItem.GetPosition(), _sortAnimationDuration, _sortAnimationEase));
            //     // newPairs[i] = new PlayerIdScoreItemPair(playerId, playerScoreItem);
            // }
            //
            // _scoreItemsMap = newPairs;
        }
    }
}
