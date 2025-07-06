using R3;
using UnityEngine;

namespace GameEngine
{
	public interface IDestroyable
	{
		Observable<GameObject> OnDead { get; }
		void Destroy();
	}
}