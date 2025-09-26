using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Visuals
{
	[RequireComponent(typeof(SpriteRenderer))]
	public sealed class ConstantSizePicture : MonoBehaviour
	{
		[SerializeField] [Required]
		private SpriteRenderer _spriteRenderer;

		[Button]
		public void SetImage(Sprite sprite)
		{
			Vector2 currentSize = _spriteRenderer.bounds.size;

			_spriteRenderer.sprite = sprite; // меняем спрайт

			// новый размер спрайта
			Vector2 newSize = _spriteRenderer.bounds.size;

			// считаем масштабный коэффициент, чтобы размеры совпали
			Vector3 scale = transform.localScale;
			scale.x *= currentSize.x / newSize.x;
			scale.y *= currentSize.y / newSize.y;
			transform.localScale = scale;
		}

		private void OnValidate()
		{
			if (_spriteRenderer == null)
			{
				_spriteRenderer = GetComponent<SpriteRenderer>();
			}
		}
	}
}