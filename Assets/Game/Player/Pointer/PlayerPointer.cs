using System.Collections.Generic;
using System.Linq;
using R3;
using R3.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Game
{
	public sealed class PlayerPointer : MonoBehaviour
	{
		[ShowInInspector]
		private readonly HashSet<GameObject> _collisions = new();
		[SerializeField] [Required]
		private Collider _collider;
		[SerializeField] [Required]
		private DecalProjector _decalProjector;
		[SerializeField]
		private float _sizeIncreaseSpeed = 5f;
		[SerializeField]
		private float _sizeDecreaseSpeed = 15f;
		[SerializeField]
		private float _maxScale;

		private Vector3 _initialScale;
		private Vector3 _decalInitialScale;
		private bool _increasing;
		private readonly CompositeDisposable _disposable = new();

		private void Awake()
		{
			_collider.OnTriggerEnterAsObservable()
			         .Subscribe(OnEnter)
			         .AddTo(_disposable);

			_collider.OnTriggerExitAsObservable()
			         .Subscribe(OnExit)
			         .AddTo(_disposable);
			_initialScale = transform.localScale;
			_decalInitialScale = _decalProjector.size;
		}

		private void LateUpdate()
		{
			if (_increasing == false)
			{
				DecreaseScale();
			}

			_increasing = false;
		}

		public IReadOnlyCollection<GameObject> GetCollisions()
		{
			foreach (var collision in _collisions.ToArray())
			{
				if (collision == null)
				{
					_collisions.Remove(collision);
				}
			}

			return _collisions;
		}

		public void IncreaseScale()
		{
			_increasing = true;
			var oldScale = transform.localScale;
			var newScale = new Vector3(
				Mathf.Min(_maxScale, oldScale.x + _sizeIncreaseSpeed * Time.deltaTime),
				oldScale.y,
				Mathf.Min(_maxScale, oldScale.z + _sizeIncreaseSpeed * Time.deltaTime)
			);
			transform.localScale = newScale;
			
			var oldDecalScale = _decalProjector.size;
			var newDecalScale = new Vector3(
				Mathf.Min(_maxScale, oldDecalScale.x + _sizeIncreaseSpeed * Time.deltaTime),
				Mathf.Min(_maxScale, oldDecalScale.y + _sizeIncreaseSpeed * Time.deltaTime),
				oldDecalScale.z
			);
			_decalProjector.size = newDecalScale;
		}

		private void DecreaseScale()
		{
			var oldScale = transform.localScale;
			var newScale = new Vector3(
				Mathf.Max(_initialScale.x, oldScale.x - _sizeDecreaseSpeed * Time.deltaTime),
				oldScale.y,
				Mathf.Max(_initialScale.z, oldScale.z - _sizeDecreaseSpeed * Time.deltaTime)
			);

			transform.localScale = newScale;
			
			var oldDecalScale = _decalProjector.size;
			var newDecalScale = new Vector3(
				Mathf.Max(_decalInitialScale.x, oldDecalScale.x - _sizeIncreaseSpeed * Time.deltaTime),
				Mathf.Max(_decalInitialScale.y, oldDecalScale.y - _sizeIncreaseSpeed * Time.deltaTime),
				oldDecalScale.z
			);
			_decalProjector.size = newDecalScale;
		}

		private void OnEnter(Collider other)
		{
			_collisions.Add(other.gameObject);
		}

		private void OnExit(Collider other)
		{
			_collisions.Remove(other.gameObject);
		}

		private void OnDestroy()
		{
			_disposable.Dispose();
		}
	}
}