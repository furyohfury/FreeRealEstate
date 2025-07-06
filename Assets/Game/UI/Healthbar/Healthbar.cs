using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public sealed class HealthBar : MonoBehaviour
	{
		[SerializeField]
		private Image _bar;
		[SerializeField] 
		private float _animationDuration = 0.2f;
		private TweenerCore<float,float,FloatOptions> _tween;

		public void SetBar(float ratio)
		{
			_tween?.Kill();
			ratio = Mathf.Clamp01(ratio);
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
