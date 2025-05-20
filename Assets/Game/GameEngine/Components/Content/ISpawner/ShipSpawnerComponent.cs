using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine
{
	[Serializable]
	public sealed class ShipSpawnerComponent
	{
		[SerializeField]
		private GameObject _prefab;
		[SerializeField]
		private Transform _worldTransform;
		[SerializeField]
		private Transform _spawnPosition;

		[Button]
		public GameObject CreateEntity()
		{
			var newEntity = Object.Instantiate(_prefab,
				_spawnPosition.position,
				_spawnPosition.rotation,
				_worldTransform);


			return newEntity;
		}
	}
}