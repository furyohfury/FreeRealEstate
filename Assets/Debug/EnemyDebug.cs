using GameEngine;
using UnityEngine;

namespace Game.Debug
{
	public class EnemyDebug : MonoBehaviour, IChangeHealth
	{
		[SerializeField]
		private LifeComponent _lifeComponent;

		public int MaxHealth => _lifeComponent.MaxHealth;
		public int CurrentHealth => _lifeComponent.CurrentHealth;

		public void ChangeHealth(int delta)
		{
			_lifeComponent.ChangeHealth(delta);
		}
	}
}