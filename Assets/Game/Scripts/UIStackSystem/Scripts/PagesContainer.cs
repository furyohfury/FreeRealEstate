using UnityEngine;

namespace UIStackSystem
{
    public sealed class PagesContainer : MonoBehaviour
    {
        public RectTransform GetContainer()
        {
            return (RectTransform)transform;
        }
    }
}