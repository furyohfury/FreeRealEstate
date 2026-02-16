using System;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public sealed class Item : MonoBehaviour
    {
        /// <summary>
        /// Only invokes by not swiped item
        /// </summary>
        public event Action<Item, Item> OnKnocked;
        public bool IsSwiped { get; set; }
        public Color Color { get; private set; }
        [SerializeField]
        private MeshRenderer _meshRenderer;
        [SerializeField]
        private Collider _collider;

        public void Move(Vector3 v)
        {
            transform.position += v;
        }

        public Tween ChangeSize(float endVal, float duration, Ease ease = Ease.Linear)
        {
            return transform.DOScale(endVal, duration).SetEase(ease);
        }

        public void SetColor(Color color)
        {
            Color = color;
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

        private void OnTriggerEnter(Collider other)
        {
            if (!IsSwiped && other.TryGetComponent(out Item item))
            {
                OnKnocked?.Invoke(item, this);
            }
        }
    }
}
