using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public sealed class ItemSystem : Singleton<ItemSystem>
    {
        [SerializeField]
        private ItemFactory _itemFactory;
        [SerializeField]
        private ItemLaneRegistry _itemLaneRegistry;
        [SerializeField]
        private ItemCollisionHandler _itemCollisionHandler;
        private readonly HashSet<Item> _activeItems = new HashSet<Item>();

        private void OnEnable()
        {
            _itemCollisionHandler.OnDestroyItem += DestroyItem;
        }

        public void DestroyItem(Item item)
        {
            if (item == null)
            {
                Debug.LogError($"Item is null when destroying");
                
                return;
            }
                
            _activeItems.Remove(item);
            Lane linkedLane = _itemLaneRegistry.GetLane(item);

            if (linkedLane != null)
            {
                _itemLaneRegistry.UnlinkItem(item, linkedLane);
                linkedLane.RemoveItem(item);
            }
            
            _itemCollisionHandler.UnsubscribeToCollisionEvents(item);
            Destroy(item.gameObject);
        }

        public Item SpawnItemAtLane(Vector3 position, Quaternion rotation, Lane linkedLane)
        {
            Item item = SpawnItem(position, rotation);
            _activeItems.Add(item);
            _itemLaneRegistry.LinkItem(item, linkedLane);
            linkedLane.AddItem(item);
            _itemCollisionHandler.SubscribeToCollisionEvents(item);

            return item;
        }

        public Item SpawnItem(Vector3 position, Quaternion rotation)
        {
            Item item = _itemFactory.SpawnRandom(position, rotation);
            
            return item;
        }

        public void InitItem(Item item, Lane linkedLane)
        {
            _activeItems.Add(item);
            _itemLaneRegistry.LinkItem(item, linkedLane);
            linkedLane.AddItem(item);
            _itemCollisionHandler.SubscribeToCollisionEvents(item);
        }

        private void OnDisable()
        {
            _itemCollisionHandler.OnDestroyItem -= DestroyItem;
        }
    }
}
