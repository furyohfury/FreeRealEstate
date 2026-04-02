using UnityEngine;

namespace Game.Utils
{
    public static class UiUtils
    {
        public static void SetPivot(RectTransform rectTransform, Vector2 pivot)
        {
            Vector2 size = rectTransform.rect.size;
            Vector2 deltaPivot = rectTransform.pivot - pivot;
            Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y) * rectTransform.localScale.x;

            // Сдвигаем pivot
            rectTransform.pivot = pivot;
            // Компенсируем позицию, чтобы объект остался на месте
            rectTransform.localPosition -= deltaPosition;
        }
    }
}
