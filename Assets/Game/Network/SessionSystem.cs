using System;
using System.Collections.Generic;
using System.Threading;
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

		public async UniTask HostPrivateSession(string playerNickname)
		{
			await LeaveCurrentSession();

			var playerProperties = GetPlayerProperties(playerNickname);
			var options = new SessionOptions()
			              {
				              MaxPlayers = 2, IsLocked = false, IsPrivate = true, PlayerProperties = playerProperties
			              }.WithRelayNetwork();
			
			ActiveSession = await MultiplayerService.Instance.CreateSessionAsync(options);
			Debug.Log($"Session {ActiveSession.Id} created! Join code: {ActiveSession.Code}");
		}
		
		public async UniTask HostPublicSession(string playerNickname)
		{
			await LeaveCurrentSession();

			var playerProperties = GetPlayerProperties(playerNickname);
			var options = new SessionOptions()
			              {
				              MaxPlayers = 2, IsLocked = false, IsPrivate = false, PlayerProperties = playerProperties
			              }.WithRelayNetwork();
			
			ActiveSession = await MultiplayerService.Instance.CreateSessionAsync(options);
			Debug.Log($"Public session {ActiveSession.Id} created!");
		}

		public async UniTask JoinSessionByCode(string code)
		{
			ActiveSession = await MultiplayerService.Instance.JoinSessionByCodeAsync(code);
			Debug.Log($"Joined session with code {code}");
		}

		public void SwitchScene(Scenes scene)
		{
			NetworkManager.Singleton.SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
		}

		// public async UniTask<IList<ISessionInfo>> FindSessions()
		// {
		// 	var options = new QuerySessionsOptions();
		// 	try
		// 	{
		// 		QuerySessionsResults result = await MultiplayerService.Instance.MatchmakeSessionAsync(options);
		// 		return result.Sessions;
		// 	}
		// 	catch (Exception e)
		// 	{
		// 		Debug.LogException(e);
		// 		return new List<ISessionInfo>();
		// 	}
		// }

		public async UniTask QuickPlay()
		{
			await LeaveCurrentSession();
			
			var quickJoinOptions = new QuickJoinOptions()
			                       {
				                       Filters = new List<FilterOption>
				                                 {
					                                 new(FilterField.AvailableSlots, "1", FilterOperation.Equal),
				                                 },
				                       Timeout = TimeSpan.FromSeconds(5)
			                       };
			
			var sessionOptions = new SessionOptions()
			                     {
				                     MaxPlayers = 2,
			                     }.WithRelayNetwork();

			ActiveSession = await MultiplayerService.Instance.MatchmakeSessionAsync(quickJoinOptions, sessionOptions);
		}

		public async UniTask LeaveCurrentSession()
		{
			if (ActiveSession != null)
			{
				if (NetworkManager.Singleton.IsHost)
				{
					Debug.Log("Already hosting...");
					return;
				}

				await ActiveSession.LeaveAsync();
				Debug.Log("In session now. Leaving...");
			}
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