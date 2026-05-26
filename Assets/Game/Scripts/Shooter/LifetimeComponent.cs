using TriInspector;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class LifetimeComponent : MonoBehaviour
    {
        [SerializeField]
        private float _lifeTime = 10f;

        [Button]
        public void Start()
        {
            Destroy(gameObject, _lifeTime);
        }
    }
}
