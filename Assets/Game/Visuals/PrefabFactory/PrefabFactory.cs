using System;
using ObjectProvide;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.Visuals
{
	public class PrefabFactory<T> : IInitializable, IDisposable where T : Object
	{
		private T _prefab;
		private readonly IObjectProvider _objectProvider;
		private readonly PrefabIdConfig<T> _prefabIdConfig;

		public PrefabFactory(IObjectProvider objectProvider, PrefabIdConfig<T> prefabIdConfig)
		{
			_objectProvider = objectProvider;
			_prefabIdConfig = prefabIdConfig;
		}

		public async void Initialize()
		{
			_prefab = await _objectProvider.Get<T>(_prefabIdConfig.Id);
		}

		public virtual T Spawn(Transform container)
		{
			return Object.Instantiate(_prefab, container);
		}

		public void Dispose()
		{
			// TODO releasing through object provider
		}
	}
}