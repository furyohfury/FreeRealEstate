using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public sealed class Lane : MonoBehaviour
    {
        [field: SerializeField]
        public float Speed { get; set; }
        public Color Color { get; private set; }
        public bool IsMoving { get; private set; } = true;
        public IReadOnlyCollection<Item> LinkedItems => _linkedItems;
        public Vector3 SpawnPos => _spawnPos.position;
        public Quaternion SpawnRot => _spawnPos.rotation;

        [SerializeField]
        private Transform _spawnPos;
        private readonly HashSet<Item> _linkedItems = new HashSet<Item>();

        public void AddItem(Item item)
        {
            _linkedItems.Add(item);
        }

        public void RemoveItem(Item item)
        {
            _linkedItems.Remove(item);
        }
    }
}
