using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public sealed class CarriableUI : MonoBehaviour
	{
		[SerializeField]
		private Image _bar;
		[SerializeField]
		private TMP_Text _text;
		[SerializeField]
		private float _animationDuration = 0.2f;
		private TweenerCore<float, float, FloatOptions> _tween;

		public void SetCount(string text)
		{
			_text.text = text;
		}

		public void SetBar(float ratio)
		{
			_tween?.Kill();
			_tween = DOTween.To(
				                () => _bar.fillAmount,
				                x => _bar.fillAmount = x,
				                ratio,
				                _animationDuration
			                )
			                .SetEase(Ease.OutCubic);
		}

		public void Hide()
		{
			_bar.enabled = false;
		}
		
		public void Show()
		{
			_bar.enabled = true;
		}

		private void OnDestroy()
		{
			_tween.Kill();
		}
	}
}