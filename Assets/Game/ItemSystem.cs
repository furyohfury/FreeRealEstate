using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public sealed class ItemSystem : MonoBehaviour
    {
        [SerializeField] private ItemFactory _itemFactory;
        private readonly HashSet<Item> _activeItems = new HashSet<Item>();

        public void DestroyItem(Item item)
        {
            _activeItems.Remove(item);
            Destroy(item.gameObject);
        }

        public Item SpawnItem(Vector3 position, Quaternion rotation)
        {
            Item item = _itemFactory.SpawnRandom(position, rotation);
            _activeItems.Add(item);

            return item;
        }
    }
}
