using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace ObjectProvide
{
	public sealed class AddressablesObjectProvider : IObjectProvider, IDisposable
	{
		private readonly Dictionary<string, AsyncOperationHandle> _cachedObjects = new();
		private readonly Dictionary<string, AsyncOperationHandle> _activeLoads = new();

		public async UniTask<T> Get<T>(string id) where T : Object
		{
			if (typeof(MonoBehaviour).IsAssignableFrom(typeof(T)))
			{
				if (_activeLoads.TryGetValue(id, out AsyncOperationHandle handle))
				{
					Debug.Log($"Dublicate task to load {id} detected, awaiting for it's finish...");
					await handle.Task;
					if (handle.Status != AsyncOperationStatus.Succeeded)
					{
						throw new ArgumentException($"Couldn't get object with id: {id}");
					}

					GameObject result = (GameObject)handle.Result;
					if (result.TryGetComponent(out T comp) == false)
					{
						throw new ArgumentException($"Loaded object doesnt have component {typeof(T)}");
					}

					return comp;
				}

				if (_cachedObjects.TryGetValue(id, out AsyncOperationHandle loadedGameObject))
				{
					GameObject result = (GameObject)loadedGameObject.Result;
					if (result.TryGetComponent(out T comp) == false)
					{
						throw new ArgumentException($"Cached object doesnt have component {typeof(T)}");
					}

					return comp;
				}

				GameObject prefab = await LoadObjectAsync<GameObject>(id);
				if (prefab.TryGetComponent(out T component) == false)
				{
					_cachedObjects.Remove(id);
					throw new ArgumentException($"Loaded object doesnt have component {typeof(T)}");
				}

				return component;
			}

			if (_activeLoads.TryGetValue(id, out var activeTask))
			{
				Debug.Log($"Dublicate task to load {id} detected, awaiting for it's finish...");
				var handle = activeTask;
				await handle.Task;
				return (T)handle.Result;
			}

			if (_cachedObjects.TryGetValue(id, out var cachedObject))
			{
				return (T)cachedObject.Result;
			}

			var loadedObject = await LoadObjectAsync<T>(id);
			return loadedObject;
		}

		private async UniTask<T> LoadObjectAsync<T>(string id) where T : Object
		{
			AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(id);
			_activeLoads.Add(id, handle);
			await handle.Task;
			if (handle.Status is AsyncOperationStatus.Failed or AsyncOperationStatus.None)
			{
				throw new ArgumentException($"Couldn't get object with id: {id}");
			}

			_activeLoads.Remove(id);
			CacheHandle(id, handle);

			return handle.Result;
		}

		private void CacheHandle<T>(string id, AsyncOperationHandle<T> loadedObject) where T : Object
		{
			_cachedObjects.Add(id, loadedObject);
		}

		public void Dispose()
		{
			foreach (var loadedObject in _cachedObjects.Values)
			{
				Addressables.Release(loadedObject);
			}
		}
	}
}