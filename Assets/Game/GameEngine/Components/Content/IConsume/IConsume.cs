using R3;
using UnityEngine;

namespace GameEngine
{
	public interface IConsume
	{
		Subject<GameObject> OnEntityConsumed { get; }
		
		void ConsumeEntity(GameObject entity);
	}
}