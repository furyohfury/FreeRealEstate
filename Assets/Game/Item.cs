using System;
using DG.Tweening;
using Game.Extensions;
using TriInspector;
using UnityEngine;

namespace Game
{
    [SelectionBase]
    [DeclareBoxGroup("Tests", Title = "Tests")]
    public sealed class Item : MonoBehaviour
    {
        /// <summary>
        /// Only invokes by not swiped item
        /// </summary>
        public event Action<CollisionEventData> OnKnocked;
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
        [Tooltip("Насколько закрашивается в gamecolor")]
        [SerializeField] [Range(0, 1f)]
        private float _coloringDegree = 1f;

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
            Color newColor = Color.Lerp(_meshRenderer.material.color, color, _coloringDegree);
            _meshRenderer.material.color = newColor;
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
                Vector3 contactPoint = _collider.ClosestPoint(other.transform.position);
                var collisionEventData = new CollisionEventData()
                                         {
                                             ControlledItem = item,
                                             HitItem = this,
                                             HitPoint = contactPoint
                                         };
                OnKnocked?.Invoke(collisionEventData);
            }
        }

        #if UNITY_EDITOR
        [Button]
        [Group("Tests")]
        private void TestColor(GameColor gameColor)
        {
            var color = gameColor.ToColor();
            Color newColor = Color.Lerp(_meshRenderer.sharedMaterial.color, color, _coloringDegree);
            _meshRenderer.sharedMaterial.color = newColor;
        }

        [Button]
        [Group("Tests")]
        private void RevertColor()
        {
            _meshRenderer.sharedMaterial.color = Color.white;
        }
        #endif
    }
}
