using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Network
{
	public sealed class SessionSystem : IInitializable
	{
		public ISession ActiveSession { get; private set; }
		private const string PLAYER_NAME_PROPERTY_KEY = "playerName";

		public async void Initialize()
		{
			try
			{
				await UnityServices.InitializeAsync();
				await AuthenticationService.Instance.SignInAnonymouslyAsync();
				Debug.Log($"Sign in anonymously succeeded! PlayerID: {AuthenticationService.Instance.PlayerId}");
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
		}

		public async UniTask StartSessionAsHost(string playerNickname)
		{
			var playerProperties = GetPlayerProperties(playerNickname);
			var options = new SessionOptions()
			              {
				              MaxPlayers = 2, IsLocked = false, IsPrivate = false, PlayerProperties = playerProperties
			              }.WithRelayNetwork();
			ActiveSession = await MultiplayerService.Instance.CreateSessionAsync(options);
			Debug.Log($"Session {ActiveSession.Id} created! Join code: {ActiveSession.Code}");
		}

		public async UniTask JoinSessionByCode(string code)
		{
			try
			{
				ActiveSession = await MultiplayerService.Instance.JoinSessionByCodeAsync(code);
				Debug.Log($"Joined session with code {code}");
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
		}

		public void SwitchScene(Scenes scene)
		{
			NetworkManager.Singleton.SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
		}

		private Dictionary<string, PlayerProperty> GetPlayerProperties(string playerNickname)
		{
			var playerName = playerNickname;
			var playerNameProperty = new PlayerProperty(playerName, VisibilityPropertyOptions.Member);
			return new Dictionary<string, PlayerProperty>
			       {
				       {
					       PLAYER_NAME_PROPERTY_KEY, playerNameProperty
				       }
			       };
		}
	}
}