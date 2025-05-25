using System;
using UnityEngine;

namespace GameEngine
{
	[RequireComponent(typeof(Collider))]
	public sealed class ProxyDamageCollider : MonoBehaviour, ITakeDamage
	{
		[SerializeField]
		private GameObject _source;

		public void TakeDamage(int delta)
		{
			if (_source.TryGetComponent(out ITakeDamage iTakeDamage) == false)
			{
				throw new NullReferenceException($"No ITakeDamage on {_source.name}");
			}
			
			iTakeDamage.TakeDamage(delta);
		}
	}
}