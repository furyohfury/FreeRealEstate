using System;
using Beatmaps;

namespace Game.BeatmapBundles
{
	[Serializable]
	public sealed class BeatmapVariant
	{
		public BeatmapConfig BeatmapConfig;
		public float SongStartTimeInSeconds;
		public float SongEndTimeInSeconds;

		public BeatmapVariant(BeatmapConfig beatmapConfig, float songStartTimeInSeconds, float songEndTimeInSeconds)
		{
			BeatmapConfig = beatmapConfig;
			SongStartTimeInSeconds = songStartTimeInSeconds;
			SongEndTimeInSeconds = songEndTimeInSeconds;
		}
	}
}