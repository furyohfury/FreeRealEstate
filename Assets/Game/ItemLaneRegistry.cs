using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ItemLaneRegistry : MonoBehaviour
    {
        private Dictionary<Item, Lane> _itemsDict = new Dictionary<Item, Lane>();

        public void LinkItem(Item item, Lane lane)
        {
            _itemsDict.Add(item, lane);
            lane.AddItem(item);
        }

        public void UnlinkItem(Item item, Lane lane)
        {
            _itemsDict.Remove(item);
            lane.RemoveItem(item);
        }

        public void SwapLane(Item item, Lane finalLane)
        {
            _itemsDict[item] = finalLane;
        }

        public Lane GetLane(Item item)
        {
            return _itemsDict[item];
        }
    }
}
