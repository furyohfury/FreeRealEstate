using TMPro;
using UnityEngine;

namespace Game.UI.Leaderboards
{
	public sealed class LeaderboardRecordView : MonoBehaviour, ILeaderboardRecordView
	{
		[SerializeField]
		private TMP_Text _position;
		[SerializeField]
		private TMP_Text _nickname;
		[SerializeField]
		private TMP_Text _score;

		public void SetPositionText(string text)
		{
			_position.SetText(text);
		}

		public void SetNicknameText(string sourceText)
		{
			_nickname.SetText(sourceText);
		}

		public void SetScoreText(string sourceText)
		{
			_score.SetText(sourceText);
		}

		public void SetParent(Transform parent)
		{
			transform.SetParent(parent);
		}

		public void Centralize()
		{
			transform.localPosition = Vector3.zero;
		}

		public void Close()
		{
			Destroy(gameObject);
		}
	}
}