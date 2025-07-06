using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class ShipVisibilityChecker : ITickable
	{
		private readonly Transform _target; // Объект в мире
		private readonly Camera _camera; // Камера
		private readonly RectTransform _canvasRect; // RectTransform канваса
		private readonly RectTransform _iconRect; // RectTransform иконки
		private readonly float _edgeOffset = 30f; // Отступ от краёв

		public ShipVisibilityChecker(Ship ship, Camera camera, Canvas canvasRect, GameObject iconRect)
		{
			_target = ship.transform;
			_camera = camera;
			_canvasRect = (RectTransform)canvasRect.transform;
			_iconRect = (RectTransform)iconRect.transform;
		}

		public void Tick()
		{
			Vector3 screenPos = _camera.WorldToScreenPoint(_target.position);

			// Проверяем, в пределах ли экрана
			if (IsOnScreen(screenPos))
			{
				_iconRect.gameObject.SetActive(false); // Скрыть иконку
			}
			else
			{
				_iconRect.gameObject.SetActive(true);

				Vector2 screenCenter = new Vector2(Screen.width, Screen.height) / 2f;
				Vector2 dir = ((Vector2)screenPos - screenCenter).normalized;

				float canvasHalfWidth = _canvasRect.rect.width / 2f - _edgeOffset;
				float canvasHalfHeight = _canvasRect.rect.height / 2f - _edgeOffset;

				// Находим позицию иконки по границе канваса
				Vector2 pos = dir * Mathf.Min(
					canvasHalfWidth / Mathf.Abs(dir.x),
					canvasHalfHeight / Mathf.Abs(dir.y)
				);

				_iconRect.anchoredPosition = pos;
			}
		}

		private bool IsOnScreen(Vector3 screenPos)
		{
			return screenPos.x >= 0 && screenPos.x <= Screen.width &&
			       screenPos.y >= 0 && screenPos.y <= Screen.height;
		}
	}
}