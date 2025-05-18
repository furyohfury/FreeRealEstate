using GameEngine;
using UnityEngine;

namespace Game
{
	public sealed class Ship : Entity
	{
		[SerializeField]
		private ConsumeEntityComponent _consumeEntityComponent;
		[SerializeField]
		private ShipSpawnerComponent _shipSpawnerComponent;
		[SerializeField]
		private ObjectsDetectorComponent _objectsDetectorComponent;

		private void Awake()
		{
			AddComponent(_consumeEntityComponent);
			AddComponent(_shipSpawnerComponent);
			AddComponent(_objectsDetectorComponent);
		}
	}
}