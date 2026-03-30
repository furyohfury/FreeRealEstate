using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public sealed class ItemHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Item _item;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _item.Highlight();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _item.DisableHighlight();
        }
    }
}
