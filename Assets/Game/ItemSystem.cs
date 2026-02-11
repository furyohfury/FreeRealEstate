using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public sealed class ItemSystem : MonoBehaviour
    {
        [SerializeField] private ItemFactory _itemFactory;
        [SerializeField] private ItemLaneRegistry _itemLaneRegistry;
        private readonly HashSet<Item> _activeItems = new HashSet<Item>();

        public void DestroyItem(Item item)
        {
            _activeItems.Remove(item);
            Lane linkedLane = _itemLaneRegistry.GetLane(item);
            _itemLaneRegistry.UnlinkItem(item, linkedLane);
            linkedLane.RemoveItem(item);
            Destroy(item.gameObject);
        }

        public Item SpawnItem(Vector3 position, Quaternion rotation, Lane linkedLane)
        {
            Item item = _itemFactory.SpawnRandom(position, rotation);
            _activeItems.Add(item);
            _itemLaneRegistry.LinkItem(item, linkedLane);

            return item;
        }
    }
}
