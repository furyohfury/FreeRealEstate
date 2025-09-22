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
		private readonly Dictionary<string, int> _counter = new();
		private readonly Dictionary<string, AsyncOperationHandle> _activeLoads = new();

		public async UniTask<T> Get<T>(string id) where T : Object
		{
			if (typeof(MonoBehaviour).IsAssignableFrom(typeof(T)))
			{
				// Being loaded
				if (_activeLoads.TryGetValue(id, out AsyncOperationHandle handle))
				{
					Debug.Log($"Duplicate task to load {id} detected, awaiting for it's finish...");
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

					if (_counter.TryAdd(id, 1) == false)
					{
						_counter[id]++;
					}

					return comp;
				}

				// Is cached
				if (_cachedObjects.TryGetValue(id, out AsyncOperationHandle loadedGameObject))
				{
					GameObject result = (GameObject)loadedGameObject.Result;
					if (result.TryGetComponent(out T comp) == false)
					{
						throw new ArgumentException($"Cached object doesnt have component {typeof(T)}");
					}

					_counter[id]++;
					return comp;
				}

				GameObject prefab = await LoadObjectAsync<GameObject>(id);
				if (prefab.TryGetComponent(out T component) == false)
				{
					_cachedObjects.Remove(id);
					throw new ArgumentException($"Loaded object doesnt have component {typeof(T)}");
				}

				if (_counter.TryAdd(id, 1) == false)
				{
					_counter[id]++;
				}

				return component;
			}

			if (_activeLoads.TryGetValue(id, out var activeTask))
			{
				Debug.Log($"Duplicate task to load {id} detected, awaiting for it's finish...");
				var handle = activeTask;
				await handle.Task;
				if (_counter.TryAdd(id, 1) == false)
				{
					_counter[id]++;
				}

				return (T)handle.Result;
			}

			if (_cachedObjects.TryGetValue(id, out var cachedObject))
			{
				return (T)cachedObject.Result;
			}

			var loadedObject = await LoadObjectAsync<T>(id);
			if (_counter.TryAdd(id, 1) == false)
			{
				_counter[id]++;
			}

			return loadedObject;
		}

		public void Release(string id)
		{
			if (_cachedObjects.TryGetValue(id, out var handle))
			{
				_counter[id]--;
				if (_counter[id] <= 0)
				{
					Addressables.Release(handle);
					_cachedObjects.Remove(id);
					_counter.Remove(id);
				}
			}
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

			_cachedObjects.Clear();
		}
	}
}