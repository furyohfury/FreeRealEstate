using System;
using System.Threading;
using Beatmaps;
using Cysharp.Threading.Tasks;
using ObjectProvide;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.Visuals
{
	public sealed class SpinnerViewFactory : IElementFactory, IAsyncStartable
	{
		private readonly IObjectProvider _objectProvider;
		private SpinnerView _prefab;

		private const string ID = "SpinnerPrefab";
		
		public SpinnerViewFactory(IObjectProvider objectProvider)
		{
			_objectProvider = objectProvider;
		}

		public async UniTask StartAsync(CancellationToken cancellation = new())
		{
			_prefab = await _objectProvider.Get<SpinnerView>(ID);
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