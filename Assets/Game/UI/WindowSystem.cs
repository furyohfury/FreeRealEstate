using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Visuals;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.UI
{
	public sealed class WindowSystem : IWindowSystem
	{
		private readonly IPrefabFactory _prefabFactory;
		private readonly Transform _container;

		private readonly Dictionary<IWindow, SpawnedWindowInfo> _windows = new();

		public WindowSystem(IPrefabFactory prefabFactory, Transform container)
		{
			_prefabFactory = prefabFactory;
			_container = container;
		}

		public async UniTask<T> Spawn<T>(int priority) where T : IWindow
		{
			if (WindowPrefabsLibrary.TryGetPrefabId<T>(out var id) == false)
			{
				throw new ArgumentException($"No type {typeof(T).FullName} in config");
			}

			var spawnedWindow = await _prefabFactory.Spawn<MonoBehaviour>(id, _container);
			if (spawnedWindow.TryGetComponent(out T iWindow) == false)
			{
				Object.Destroy(spawnedWindow);
				_prefabFactory.Return(id);
				throw new ArgumentException($"No type {typeof(T).FullName} on spawned object");
			}

			// Sorting first so dont have to check for same instance while sorting
			SortWindows(priority, spawnedWindow);
			_windows.Add(iWindow, new SpawnedWindowInfo(spawnedWindow.transform, priority, id));

			return iWindow;
		}

		private void SortWindows(int priority, MonoBehaviour spawnedWindow)
		{
			foreach (var info in _windows.Values.OrderBy(info => info.Priority))
			{
				if (priority <= info.Priority)
				{
					spawnedWindow.transform.SetSiblingIndex(info.Transform.GetSiblingIndex());
					break;
				}
			}
		}

		public void Close(IWindow window)
		{
			if (window is not IWindowClosable closable)
			{
				return;
			}

			var info = _windows[window];
			_prefabFactory.Return(info.PrefabId);
			_windows.Remove(window);
			closable.Close();
		}

		public void CloseAll()
		{
			foreach (var window in _windows.Keys.ToArray())
			{
				Close(window);
			}
		}

		private class SpawnedWindowInfo
		{
			public readonly Transform Transform;
			public readonly int Priority;
			public readonly string PrefabId;

			public SpawnedWindowInfo(Transform transform, int priority, string prefabId)
			{
				Transform = transform;
				Priority = priority;
				PrefabId = prefabId;
			}
		}
	}
}