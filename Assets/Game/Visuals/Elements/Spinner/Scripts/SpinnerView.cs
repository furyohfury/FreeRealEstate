using DG.Tweening;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class SpinnerView : ElementView
	{
		[SerializeField]
		private SpriteRenderer _spriteRenderer;
		[SerializeField]
		private float _animationDuration = 0.5f;
		[SerializeField]
		private float _enlargeScale = 5f;

		public Tween EnlargeAnimation()
		{
			return transform.DOScale(_enlargeScale, _animationDuration);
		}

		public Tween FadeToAnimation(float alpha)
		{
			return _spriteRenderer.DOFade(alpha, _animationDuration);
		}

		public Tween MoveToAnimation(Vector3 point)
		{
			return transform.DOMove(point, _animationDuration);
		}
	}
}