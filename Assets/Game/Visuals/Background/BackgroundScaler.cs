using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Game.Visuals
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class BackgroundScaler : MonoBehaviour
	{
		[SerializeField] 
		private SpriteRenderer _spriteRenderer;
		private Camera _camera;

		[Inject]
		public void Construct(Camera camera)
		{
			_camera = camera;
		}

		private void Start()
		{
			SetScaleForScreen();
		}

		[Button]
		private void SetScaleForScreen()
		{
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			if (sr == null || sr.sprite == null) return;

			// Сбрасываем масштаб, чтобы получить корректный sprite.bounds
			transform.localScale = Vector3.one;

			// Размеры камеры
			float worldScreenHeight = _camera.orthographicSize * 2f;
			float worldScreenWidth = worldScreenHeight * _camera.aspect;

			// Размер спрайта (без учёта transform.localScale)
			Vector2 spriteSize = sr.sprite.bounds.size;

			// Рассчитываем масштаб
			float scaleX = worldScreenWidth / spriteSize.x;
			float scaleY = worldScreenHeight / spriteSize.y;

			// Берём наибольший, чтобы спрайт точно закрыл экран
			float finalScale = Mathf.Max(scaleX, scaleY);
			transform.localScale = new Vector3(scaleX, scaleY, 1);
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