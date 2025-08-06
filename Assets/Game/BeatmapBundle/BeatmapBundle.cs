using System.Collections.Generic;
using System.Linq;
using Beatmaps;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.BeatmapBundles
{
	[CreateAssetMenu(fileName = "BeatmapBundle", menuName = "Beatmap/BeatmapBundle")]
	public sealed class BeatmapBundle : ScriptableObject
	{
		public string Name => _name;
		public string SongId => _songId;
		public string BackgroundId => _backgroundId;
		public BeatmapVariant[] BeatmapsVariants => _beatmapsVariants;

		public BeatmapBundle(string name, string songId, string backgroundId, BeatmapVariant[] beatmapsVariants)
		{
			_name = name;
			_songId = songId;
			_backgroundId = backgroundId;
			_beatmapsVariants = beatmapsVariants;
		}

		[SerializeField]
		private string _name;
		[SerializeField]
		private string _songId;
		[SerializeField]
		private string _backgroundId;
		[SerializeField]
		private BeatmapVariant[] _beatmapsVariants;

		[Button]
		private void AddVariant(BeatmapConfig beatmapConfig, float startTime, float endTime)
		{
			List<BeatmapVariant> beatmapVariants = _beatmapsVariants.ToList();
			beatmapVariants.Add(new BeatmapVariant(beatmapConfig, startTime, endTime));
			_beatmapsVariants = beatmapVariants.ToArray();
		}
	}
}