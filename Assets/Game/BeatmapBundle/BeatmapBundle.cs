using System;
using Beatmaps;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.BeatmapBundle
{
	[CreateAssetMenu(fileName = "BeatmapBundle", menuName = "BeatmapBundle")]
	public sealed class BeatmapBundle : SerializedScriptableObject
	{
		[SerializeField]
		private string _name;
		[SerializeField]
		private AudioClip _song;
		[SerializeField]
		private Sprite _background;
		[SerializeField]
		private BeatmapVariant[] _beatmapsVariants;

		[Serializable]
		private struct BeatmapVariant
		{
			public BeatmapConfig BeatmapConfig;
			public DifficultyConfig DifficultyConfig;
			public float SongStartTimeInSeconds;
			public float SongEndTimeInSeconds;
		}
	}
}