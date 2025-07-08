using Game;
using UnityEngine;

namespace GameEngine
{
	public interface IMoveable
	{
		Vector3 Position { get; }
		void Move(Vector3 direction);
		void MoveTo(Vector3 position);
	}
}