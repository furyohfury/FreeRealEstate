using System;
using Beatmaps;
using ObjectProvide;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.Visuals
{
	public sealed class SpinnerViewFactory : IElementFactory, IStartable
	{
		private readonly IObjectProvider _objectProvider;
		private readonly SpinnerPrefabConfig _config;
		private SpinnerView _prefab;

		public SpinnerViewFactory(IObjectProvider objectProvider, SpinnerPrefabConfig config)
		{
			_objectProvider = objectProvider;
			_config = config;
		}

		public async void Start()
		{
			_prefab = await _objectProvider.Get<SpinnerView>(_config.SpinnerViewId);
		}

		public Type GetElementType()
		{
			return typeof(Spinner);
		}

		public ElementView Spawn(MapElement element, Transform parent)
		{
			return Object.Instantiate(_prefab, parent);
		}
	}
}