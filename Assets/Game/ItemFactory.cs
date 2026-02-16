using UnityEngine;

namespace Game
{
    public sealed class ItemFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform _container;
        [SerializeField]
        private Item[] _prefabs;
        private int _counter = 0;

        public Item SpawnRandom(Vector3 position, Quaternion rotation)
        {
            Item newItem = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], position, rotation, _container);
            newItem.gameObject.name = $"Item_{_counter++}";
            
            return newItem;
        }
    }
}
