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
		public AudioClip SongClip => _songClip;
		public Sprite Background => _background;
		public BeatmapVariant[] BeatmapsVariants => _beatmapsVariants;

		public BeatmapBundle(string name, string songId, Sprite background, BeatmapVariant[] beatmapsVariants)
		{
			_name = name;
			_songId = songId;
			_background = background;
			_beatmapsVariants = beatmapsVariants;
		}

		[SerializeField]
		private string _name;
		[SerializeField]
		private string _songId;
		[SerializeField]
		private AudioClip _songClip;
		[SerializeField]
		private Sprite _background;
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