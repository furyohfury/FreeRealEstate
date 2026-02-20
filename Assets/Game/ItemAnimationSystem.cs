using DG.Tweening;
using UnityEngine;

namespace Game
{
    public sealed class ItemAnimationSystem : Singleton<ItemAnimationSystem>
    {
        [SerializeField]
        private Ease _scaleAnimEase = Ease.OutCirc;
        [SerializeField]
        private float _scaleAnimDuration = 1f;

        public Tween ScaleOnKnockAnim(Transform objTransform)
        {
            return objTransform.DOScale(0, _scaleAnimDuration)
                               .SetEase(_scaleAnimEase);
        }
    }
}
