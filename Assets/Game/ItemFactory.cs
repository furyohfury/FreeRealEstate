using UnityEngine;

namespace Game
{
    public sealed class ItemFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform _container;
        [SerializeField]
        private Item[] _prefabs;

        public Item SpawnRandom(Vector3 position, Quaternion rotation)
        {
            return Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], position, rotation, _container);
        }
    }
}
