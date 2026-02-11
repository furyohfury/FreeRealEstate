using DG.Tweening;
using UnityEngine;

namespace Game
{
    public sealed class Item : MonoBehaviour
    {
        public Color Color { get; private set; }
        [SerializeField]
        private MeshRenderer _meshRenderer;

        public void Move(Vector3 v)
        {
            transform.position += v;
        }

        public Tween MoveTo(Vector3 v, float duration, Ease ease =  Ease.Linear)
        {
            if (duration <= 0f)
            {
                Move(v);
                return null;
            }
            
            return transform.DOMove(v, duration)
                     .SetEase(ease);
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
    }
}
