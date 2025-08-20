using UnityEngine;

namespace Game.Scoring
{
	[CreateAssetMenu(
		fileName = nameof(ScoreResultConfig),
		menuName = nameof(Scoring) + "/" + nameof(ScoreResultConfig))]
	public class ScoreResultConfig : ScriptableObject
	{
		public ScoreResult[] ScoreResults => _scoreResults;
		[SerializeField]
		private ScoreResult[] _scoreResults;
	}
}