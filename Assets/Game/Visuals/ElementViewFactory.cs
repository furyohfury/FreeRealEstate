using System;
using System.Collections.Generic;
using Beatmaps;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class ElementViewFactory
	{
		private readonly Dictionary<Type, IElementFactory> _factories = new();

		public ElementViewFactory(IEnumerable<IElementFactory> factories)
		{
			foreach (var factory in factories)
			{
				if (_factories.TryAdd(factory.GetElementType(), factory) == false)
				{
					throw new ArgumentException("Dublicate factories");
				}
			}
		}

		public ElementView Spawn(MapElement element, Transform parent)
		{
			var type = element.GetType();
			if (_factories.TryGetValue(type, out var factory))
			{
				return factory.Spawn(element, parent);
			}

			throw new NullReferenceException($"No factory for {type}");
		}
	}
}