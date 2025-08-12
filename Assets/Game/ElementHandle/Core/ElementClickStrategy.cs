using System;
using System.Collections.Generic;
using Beatmaps;
using Game.SongMapTime;

namespace Game.ElementHandle
{
	public abstract class ElementClickStrategy
	{
		protected IMapTime MapTime;

		protected ElementClickStrategy(IMapTime mapTime)
		{
			MapTime = mapTime;
		}

		public abstract Type GetElementType();
		public abstract ClickResult HandleClick(MapElement element, Notes inputNote);
		public abstract void SetDifficultyParameters(IEnumerable<IDifficultyParams> parameters);
	}
}