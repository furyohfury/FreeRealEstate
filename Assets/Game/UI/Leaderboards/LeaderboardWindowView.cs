using TMPro;
using UnityEngine;

namespace Game.UI.Leaderboards
{
	public sealed class LeaderboardWindowView : MonoBehaviour, ILeaderboardWindowView
	{
		[SerializeField]
		private TMP_Text _beatmapTitle;
		[SerializeField]
		private Transform _recordsContainer;
		[SerializeField]
		private Transform _playerScoreContainer;

		public void SetBeatmapTitleText(string sourceText)
		{
			_beatmapTitle.SetText(sourceText);
		}

		public void AddRecord(ILeaderboardRecordView leaderboardRecordView)
		{
			leaderboardRecordView.SetParent(_recordsContainer);
			leaderboardRecordView.Centralize();
		}

		public void AddPlayerScoreRecord(ILeaderboardRecordView leaderboardRecordView)
		{
			leaderboardRecordView.SetParent(_playerScoreContainer);
			leaderboardRecordView.Centralize();
		}

		public void Close()
		{
			Destroy(gameObject);
		}
	}
}