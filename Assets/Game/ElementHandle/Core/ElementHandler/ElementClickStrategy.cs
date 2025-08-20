using System;
using System.Collections.Generic;
using Beatmaps;
using Game.BeatmapTime;

namespace Game.ElementHandle
{
	public abstract class ElementClickStrategy : IElementHandler
	{
		protected IMapTime MapTime;

		protected ElementClickStrategy(IMapTime mapTime)
		{
			MapTime = mapTime;
		}

		public abstract Type GetElementType();
		public abstract HandleResult HandleClick(MapElement element, Notes inputNote);
		public abstract void SetDifficultyParameters(IEnumerable<IDifficultyParams> parameters);
	}
}