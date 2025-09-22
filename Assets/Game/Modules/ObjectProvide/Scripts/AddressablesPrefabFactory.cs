using Cysharp.Threading.Tasks;
using ObjectProvide;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Visuals
{
	public class AddressablesPrefabFactory : IPrefabFactory
	{
		private readonly AddressablesObjectProvider _objectProvider;

		public AddressablesPrefabFactory(AddressablesObjectProvider objectProvider)
		{
			_objectProvider = objectProvider;
		}

		public virtual async UniTask<T> Spawn<T>(string id, Transform container) where T : MonoBehaviour
		{
			var prefab = await _objectProvider.Get<T>(id);
			var spawnedObject = Object.Instantiate(prefab, container);
			spawnedObject.name = typeof(T).ToString();
			var releaser = spawnedObject.gameObject.AddComponent<AddressablesReleaser>();
			releaser.Init(this, id);
			return spawnedObject;
		}

		public void Return(string id)
		{
			_objectProvider.Release(id);
		}
	}
}