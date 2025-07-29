using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class ActiveSpinnerView : MonoBehaviour
	{
		[SerializeField] [BoxGroup("OuterRing")]
		private Transform _outerRing;
		[SerializeField] [BoxGroup("InnerRing")]
		private Transform _innerRing;
		[SerializeField] [BoxGroup("Text")]
		private TMP_Text _tmpText;
		[SerializeField] [BoxGroup("OuterRing")]
		private float _outerRingShakeDuration = 0.05f;
		[SerializeField] [BoxGroup("OuterRing")]
		private float _outerRingShakeFinalScaleMultiplier = 1.1f;
		private Vector3 _outerRingInitialScale;
		private Sequence _outerRingShakeSequence;

		[SerializeField] [BoxGroup("InnerRing")]
		private float _innerRingScaleChangeDuration = 0.07f;

		[SerializeField] [BoxGroup("Text")]
		private float _textShakeDuration = 0.05f;
		[SerializeField] [BoxGroup("Text")]
		private float _textShakeFinalScaleMultiplier = 1.1f;
		private Sequence _textShakeSequence;
		private Vector3 _textInitialScale;

		private Vector3 _innerRingInitialScale;

		private void Awake()
		{
			_outerRingInitialScale = _outerRing.localScale;
			_textInitialScale = _tmpText.transform.localScale;
			_innerRingInitialScale = _innerRing.localScale;
		}

		[Button] [BoxGroup("OuterRing")]
		public void BounceOuterRing()
		{
			if (_outerRingShakeSequence != null)
			{
				return;
			}

			_outerRingShakeSequence = BounceSequence(
					_outerRing,
					_outerRingShakeDuration,
					_outerRingInitialScale
					, _outerRingInitialScale * _outerRingShakeFinalScaleMultiplier
				)
				.AppendCallback(() => _outerRingShakeSequence = null);
		}

		[Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)] [BoxGroup("InnerRing")]
		public void SetInnerRingScaleRatio(float ratio)
		{
			var finalScale = _innerRingInitialScale * ratio;
			_innerRing.DOScale(finalScale, _innerRingScaleChangeDuration);
		}

		public void SetText(string text)
		{
			_tmpText.SetText(text);
		}

		[Button] [BoxGroup("Text")]
		public void BounceText()
		{
			if (_textShakeSequence != null)
			{
				return;
			}

			_textShakeSequence = BounceSequence(
					_tmpText.transform,
					_textShakeDuration,
					_textInitialScale
					, _textInitialScale * _textShakeFinalScaleMultiplier
				)
				.AppendCallback(() => _textShakeSequence = null);
		}

		public void Destroy()
		{
			Destroy(gameObject);
		}

		private Sequence BounceSequence(Transform elementTransform, float duration, Vector3 initialScale, Vector3 endScale)
		{
			return DOTween.Sequence()
			              .Append(elementTransform.DOScale(
				              endScale,
				              duration / 2)
			              )
			              .Append(elementTransform.DOScale(
					              initialScale,
					              duration / 2
				              )
			              );
		}
	}
}