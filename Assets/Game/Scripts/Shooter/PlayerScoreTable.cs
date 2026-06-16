using UnityEngine;

namespace Game
{
    public sealed class RoundInfoUI : MonoBehaviour
    {
        public Transform Container => _container;

        [Header("Parameters")]
        [SerializeField]
        private float _sortAnimationDuration;
        [Header("References")]
        [SerializeField]
        private Transform _container;
    }
}
