using DG.Tweening;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
	public sealed class CellGameObjectView : CellView, IPointerClickHandler
	{
		[SerializeField]
		private SpriteRenderer _spriteRenderer;
		[SerializeField]
		private ParticleSystem _rightGuessParticles;

		[Header("Bounce")] [SerializeField]
		private bool _bounceOnAwake;
		[SerializeField]
		private float _timeToEnlargedState = 0.3f;
		[SerializeField]
		private float _enlargedScale = 1.20f;
		[SerializeField]
		private float _reducedScale = 0.95f;
		[SerializeField]
		private float _timeToReducedState = 0.05f;
		[SerializeField]
		private float _timeToNormalState = 0.05f;

		[Header("EaseInBounce")] [SerializeField]
		private float _easeReducedScale = 0.5f;
		[SerializeField]
		private float _easeTimeToReducedState = 1f;
		[SerializeField]
		private float _easeTimeToNormalState = 1f;

		private Sequence _sequence;

		private void Awake()
		{
			if (_bounceOnAwake)
			{
				transform.localScale = new Vector3(0, 0, 0);
				Bounce();
			}
		}

		public override void DoWrongChoiceEffect()
		{
			EaseInBounce();
		}

		public override void DoCorrectChoiceEffect()
		{
			Bounce();
			var particles = Instantiate(_rightGuessParticles, transform.position, transform.rotation);
			var particlesMain = particles.main;
			particlesMain.playOnAwake = true;
			particlesMain.stopAction = ParticleSystemStopAction.Destroy;
		}

		[Button]
		private void Bounce()
		{
			if (_sequence == null)
			{
				_sequence = DOTween.Sequence()
				                   .Append(transform.DOScale(new Vector3(_enlargedScale, _enlargedScale, 0), _timeToEnlargedState))
				                   .Append(transform.DOScale(new Vector3(_reducedScale, _reducedScale, 0), _timeToReducedState))
				                   .Append(transform.DOScale(new Vector3(1, 1, 0), _timeToNormalState))
				                   .OnComplete(() => _sequence = null);
			}
		}

		[Button]
		private void EaseInBounce()
		{
			if (_sequence == null)
			{
				_sequence = DOTween.Sequence()
				                   .Append(transform.DOScale(new Vector3(_easeReducedScale, _easeReducedScale, 0), _easeTimeToReducedState))
				                   .Append(transform.DOScale(new Vector3(1, 1, 0), _easeTimeToNormalState))
				                   .SetEase(Ease.InBounce)
				                   .OnComplete(() => _sequence = null);
			}
		}

		public override void SetSprite(Sprite sprite)
		{
			_spriteRenderer.sprite = sprite;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			OnClicked.OnNext(Unit.Default);
		}

		private void OnDestroy()
		{
			_sequence?.Kill();
		}
	}
}