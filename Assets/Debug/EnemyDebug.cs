using GameEngine;
using UnityEngine;

namespace Game
{
	public class EnemyDebug : MonoBehaviour, ITakeDamage
	{
		// [SerializeField]
		// private LifeComponent _lifeComponent;
		//
		// public int MaxHealth => _lifeComponent.MaxHealth;
		// public int CurrentHealth => _lifeComponent.CurrentHealth;
		//
		public void TakeDamage(int delta)
		{
			// _lifeComponent.ChangeHealth(delta);
		}
	}
}