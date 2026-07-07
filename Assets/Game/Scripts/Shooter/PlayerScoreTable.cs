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

        private Sequence _activeMoveTween;
        private readonly List<PlayerScoreItem> _scoreItems = new List<PlayerScoreItem>();

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
            _activeMoveTween?.Complete();
            _verticalLayoutGroup.enabled = true;
            PlayerScoreItem scoreItem = Instantiate(_playerScoreItemPrefab, _container);
            _scoreItems.Add(scoreItem);
            Debug.Log($"Create score item for: {scoreData.PlayerData.Nickname}");
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_verticalLayoutGroup.transform);
            _verticalLayoutGroup.enabled = false;

            return scoreItem;
        }

        public void SortItems(IList<PlayerScoreItem> playerScoreItems)
        {
            _verticalLayoutGroup.enabled = false;
            _activeMoveTween?.Complete();
            _activeMoveTween = DOTween.Sequence();

            // Запоминаем текущие позиции элементов
            Dictionary<PlayerScoreItem, Vector3> positions = new Dictionary<PlayerScoreItem, Vector3>(_scoreItems.Count);
            foreach (PlayerScoreItem item in _scoreItems)
            {
                positions[item] = item.GetPosition();
            }

            // Анимируем перемещение
            for (int i = 0; i < playerScoreItems.Count; i++)
            {
                Debug.Log($"internal list count : {_scoreItems.Count}, parameter playerscoreitemsc count : {playerScoreItems.Count}");
                PlayerScoreItem item = playerScoreItems[i];
                Vector3 targetPosition = positions[_scoreItems[i]];

                _activeMoveTween.Join(item.Move(targetPosition, _sortAnimationDuration, _sortAnimationEase));
            }

            _activeMoveTween.OnComplete(() =>
            {
                // Обновляем порядок в иерархии
                _scoreItems.Clear();

                for (int i = 0; i < playerScoreItems.Count; i++)
                {
                    PlayerScoreItem item = playerScoreItems[i];
                    item.transform.SetSiblingIndex(i);
                    item.transform.localPosition = Vector3.zero;
                    _scoreItems.Add(item);
                }

                _verticalLayoutGroup.enabled = true;
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_container);
                _activeMoveTween = null;
            });
        }
    }
}
