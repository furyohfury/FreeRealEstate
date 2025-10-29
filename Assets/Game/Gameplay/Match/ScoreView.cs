using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
	public sealed class ScoreView : NetworkBehaviour
	{
		[SerializeField]
		private VerticalLayoutGroup _verticalLayoutGroup;
		[SerializeField]
		private TMP_Text _p1Name;
		[SerializeField]
		private TMP_Text _p2Name;

		public override void OnNetworkSpawn()
		{
			_verticalLayoutGroup.reverseArrangement = !IsHost;
		}

		public void SetP1Name(string text)
		{
			_p1Name.SetText(text);
		}

		public void SetP2Name(string text)
		{
			_p2Name.SetText(text);
		}
	}
}