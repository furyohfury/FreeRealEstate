using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine
{
	[Serializable]
	public sealed class ShipSpawnerComponent : IComponent
	{
		[SerializeField]
		private Entity _prefab;
		[SerializeField]
		private Transform _worldTransform;
		[SerializeField]
		private Transform _spawnDirection;
		
		[Button]
		public Entity CreateEntity()
		{
			var newEntity = Object.Instantiate(_prefab, _worldTransform);
			if (newEntity.TryGetComponent(out MoveRigidbodyComponent moveComponent) == false)
			{
				throw new NullReferenceException($"No move component on {_prefab.gameObject.name} prefab");
			}
			
			moveComponent.MoveTo(_spawnDirection.position);

			return newEntity;
		}
	}
}