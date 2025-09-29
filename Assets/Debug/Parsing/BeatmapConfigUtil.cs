using Beatmaps;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameDebug.Parsing
{
	[CreateAssetMenu(fileName = "BeatmapConfigUtil", menuName = "Osu parsing/BeatmapConfigUtil")]
	public class BeatmapConfigUtil : ScriptableObject
	{
		[SerializeField]
		private BeatmapConfig _beatmapConfig;
		[SerializeField]
		private TextAsset _textAsset;

		[Button]
		public void FillConfigByParsing()
		{
			_beatmapConfig.SetBeatmap(OsuBeatmapParser.Parse(_textAsset.text));
		}
	}
}