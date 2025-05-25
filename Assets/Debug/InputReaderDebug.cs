using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game
{
	public class InputReaderDebug : MonoBehaviour
	{
		private PlayerInputReader _playerInputReader;

		[Inject]
		private void Construct(PlayerInputReader playerInputReader)
		{
			_playerInputReader = playerInputReader;
		}

		[Button]
		private void InitReader()
		{
			_playerInputReader.Initialize();
			var message = _playerInputReader.OnLookAction == null;
			Debug.Log(message.ToString());
		}
	}
}