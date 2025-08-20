using Game.Scoring;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace GameDebug.Score
{
	public class ScoreDebug : MonoBehaviour
	{
		[Inject]
		private ScoreSystem _scoreSystem;

		[ShowInInspector]
		public int Score => _scoreSystem.Score.CurrentValue;
	}
}