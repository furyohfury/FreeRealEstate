using R3;
using UnityEngine;

namespace GameEngine
{
	public interface IConsume
	{
		Observable<GameObject> OnConsumeEnd { get; }
		
		void ConsumeEntity(GameObject entity);
	}
}