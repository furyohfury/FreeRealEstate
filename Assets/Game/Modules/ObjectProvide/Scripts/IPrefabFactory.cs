using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Visuals
{
	public interface IPrefabFactory
	{
		UniTask<T> Spawn<T>(string id, Transform container) where T : MonoBehaviour;
		void Return(string id);
	}
}