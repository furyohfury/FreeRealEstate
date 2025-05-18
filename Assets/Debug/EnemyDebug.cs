using GameEngine;
using UnityEngine;

namespace Game.Debug
{
	public class EnemyDebug : Entity
	{
		[SerializeField]
		private LifeComponent _lifeComponent;

		private void Awake()
		{
			AddComponent(_lifeComponent);
		}
	}
}