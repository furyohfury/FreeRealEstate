using System;
using Beatmaps;
using UnityEngine;

namespace Game.Visuals
{
	public interface IElementFactory
	{
		Type GetElementType();
		ElementView Spawn(MapElement element, Transform parent);
	}
}