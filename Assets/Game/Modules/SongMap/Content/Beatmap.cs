using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Beatmaps
{
	[Serializable]
	public sealed class Beatmap : IBeatmap
	{
		public int Bpm
		{
			get => _bpm;
			set => _bpm = value;
		}
		public List<MapElement> MapElements
		{
			get => _mapElements;
			set => _mapElements = value;
		}
		public Difficulty Difficulty
		{
			get => _difficulty;
			set => _difficulty = value;
		}

		[SerializeField]
		private int _bpm = 120;
		[SerializeReference]
		private List<MapElement> _mapElements;
		[SerializeField]
		private Difficulty _difficulty;

		public Beatmap(int bpm, IList<MapElement> mapElements, Difficulty difficulty)
		{
			_bpm = bpm;
			_mapElements = mapElements.ToList();
			_difficulty = difficulty;
		}

		public int GetBpm()
		{
			return _bpm;
		}

		public IDifficulty GetDifficulty()
		{
			return _difficulty;
		}

		public MapElement[] GetMapElements()
		{
			return _mapElements.ToArray();
		}

		[Button]
		private void AddElement(ElementType type)
		{
		}

		private enum ElementType
		{
			SingleNote
			, Spinner
			, Drumroll
		}
	}
}