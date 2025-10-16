using Game.App;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameDebug
{
	public sealed class EnterNameMenuDebug : MonoBehaviour
	{
		[ShowInInspector]
		private string Nickname => _playerNickname.Nickname;
		
		[Inject]
		private PlayerNickname _playerNickname;
	}
}