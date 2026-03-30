using System;
using DG.Tweening;
using Game.Extensions;
using TriInspector;
using UnityEngine;

namespace Game
{
    [SelectionBase]
    public sealed class Item : MonoBehaviour
    {
        /// <summary>
        /// Only invokes by not swiped item
        /// </summary>
        public event Action<Item, Item> OnKnocked;
        public bool IsPlayerControlled { get; set; }
        public GameColor GameColor { get; private set; }
        public Collider Collider => _collider;
        [SerializeField]
        private MeshRenderer _meshRenderer;
        [SerializeField]
        private Collider _collider;
        [SerializeField]
        [Range(0, 1)]
        private float _highlightWhiteDegree;

        public void Move(Vector3 direction)
        {
            transform.position += direction;
        }

        public Tween ChangeSize(float endVal, float duration, Ease ease = Ease.Linear)
        {
            return transform.DOScale(endVal, duration).SetEase(ease);
        }

        [Button]
        public void SetColor(GameColor color)
        {
            GameColor = color;
            SetVisualColor(color);
        }

        private void SetVisualColor(GameColor color)
        {
            SetVisualColor(color.ToColor());
        }

        private void SetVisualColor(Color color)
        {
            _meshRenderer.material.color = color;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void EnableCollision()
        {
            _collider.enabled = true;
        }

        public void DisableCollision()
        {
            _collider.enabled = false;
        }

        [Button]
        public void Highlight()
        {
            Color newColor = Color.Lerp(_meshRenderer.material.color, Color.white, _highlightWhiteDegree);
            _meshRenderer.material.color = newColor;
        }
        
        [Button]
        public void DisableHighlight()
        {
            SetColor(GameColor);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsPlayerControlled
                && other.TryGetComponent(out Item item))
            {
                OnKnocked?.Invoke(item, this);
            }
        }
    }
}
