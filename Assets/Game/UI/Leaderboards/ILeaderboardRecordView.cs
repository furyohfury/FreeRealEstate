using UnityEngine;

namespace Game.UI.Leaderboards
{
	public interface ILeaderboardRecordView : IWindow, IWindowClosable
	{
		void SetPositionText(string text);
		void SetNicknameText(string sourceText);
		void SetScoreText(string sourceText);
		void SetParent(Transform parent);
		void Centralize();
	}
}