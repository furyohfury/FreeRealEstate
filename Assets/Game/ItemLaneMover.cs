using System;
using System.Collections.Generic;
using System.Linq;
using TriInspector;
using UnityEngine;

namespace Game
{
    public sealed class ItemLaneMover : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed;
        private List<Item> _items = new List<Item>();

        [Button]
        private void GetFromScene()
        {
            _items = FindObjectsByType<Item>(FindObjectsSortMode.None).ToList();
        }

        private void Update()
        {
            foreach (var item in _items)
            {
                item.Move(Vector3.back * (_moveSpeed * Time.deltaTime));
            }
        }
    }
}
