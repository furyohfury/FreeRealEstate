using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public sealed class Lane : MonoBehaviour
	{
		[field: SerializeField]
		public float Speed { get; set; }
		[field: SerializeField]
		public Color Color { get; private set; }
		public bool IsMoving { get; private set; } = true;
		public Vector3 SpawnPos => _spawnPos.position;
		public Quaternion SpawnRot => _spawnPos.rotation;
		[field: SerializeField]
		public int Number { get; set; }

		[SerializeField]
		private Transform _spawnPos;
		public readonly HashSet<Item> LinkedItems = new HashSet<Item>();

		public void AddItem(Item item)
		{
			LinkedItems.Add(item);
		}

		public void RemoveItem(Item item)
		{
			LinkedItems.Remove(item);
		}
	}
}
