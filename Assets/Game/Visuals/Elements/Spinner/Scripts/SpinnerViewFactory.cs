using System;
using Beatmaps;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Visuals
{
	public sealed class SpinnerViewFactory : IElementFactory
	{
		private readonly SpinnerView _prefab;

		public SpinnerViewFactory(SpinnerView prefab)
		{
			_prefab = prefab;
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