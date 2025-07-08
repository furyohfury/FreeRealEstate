using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace GameEngine
{
	[Serializable]
	public sealed class ShipSpawnerComponent
	{
		[SerializeField]
		private GameObject _prototype;
		[SerializeField]
		private Transform _container;
		[SerializeField]
		private Transform _spawnPoint;
		[SerializeField]
		private float _spawnPositionOffset;

		[Button]
		public GameObject CreateEntity()
		{
			var pos = new Vector3(
				_spawnPoint.position.x + Random.Range(0, _spawnPositionOffset),
				_spawnPoint.position.y,
				_spawnPoint.position.z + Random.Range(0, _spawnPositionOffset
				));
			
			var newEntity = Object.Instantiate(
				_prototype,
				pos,
				_spawnPoint.rotation,
				_container
				);
			newEntity.SetActive(true);

			return newEntity;
		}
	}
}