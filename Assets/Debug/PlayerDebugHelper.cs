using System;
using Cysharp.Threading.Tasks;
using Game.Network;
using Gameplay;
using R3;
using Unity.Services.Multiplayer;
using UnityEngine;
using Zenject;

namespace GameDebug
{
	public sealed class PlayerDebugHelper : MonoBehaviour
	{
		[Inject]
		private MyPlayerService _myPlayerService;
		[Inject]
		private SessionSystem _sessionSystem;
		private IDisposable _disposable;

		private void OnEnable()
		{
			_disposable = _sessionSystem.OnSessionStarted
			                            .Subscribe(OnClientConnectedCallback);
		}

		private async void OnClientConnectedCallback(ISession obj)
		{
			await UniTask.Delay(500);
			Debug.Log($"My player is {_myPlayerService.MyPlayer.ToString()}");
		}

		private void OnDisable()
		{
			_disposable.Dispose();
		}
	}
}