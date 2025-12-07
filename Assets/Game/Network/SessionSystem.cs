using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
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
		public Observable<ISession> OnSessionStarted => _onSessionStarted;
		private Subject<ISession> _onSessionStarted = new Subject<ISession>();
		public ISession ActiveSession { get; private set; }

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
			await CheckForExistingSession();

			var playerProperties = GetPlayerProperties(playerNickname);
			var options = new SessionOptions()
			              {
				              MaxPlayers = 2, IsLocked = false, IsPrivate = true, PlayerProperties = playerProperties
			              }.WithRelayNetwork();
			
			ActiveSession = await MultiplayerService.Instance.CreateSessionAsync(options);
			_onSessionStarted.OnNext(ActiveSession);
			Debug.Log($"Session {ActiveSession.Id} created! Join code: {ActiveSession.Code}");
		}
		
		public async UniTask<ISession> HostPublicSession(string playerNickname)
		{
			await CheckForExistingSession();

			var playerProperties = GetPlayerProperties(playerNickname);
			var options = new SessionOptions()
			              {
				              MaxPlayers = 2, IsLocked = false, IsPrivate = false, PlayerProperties = playerProperties
			              }.WithRelayNetwork();
			
			ActiveSession = await MultiplayerService.Instance.CreateSessionAsync(options);
			_onSessionStarted.OnNext(ActiveSession);
			Debug.Log($"Public session {ActiveSession.Id} created!");
			return ActiveSession;
		}

		public async UniTask<ISession> JoinSessionByCode(string code, string nickname)
		{
			var joinSessionOptions = new JoinSessionOptions()
			                         {
				                         PlayerProperties = GetPlayerProperties(nickname)
			                         };
			
			ActiveSession = await MultiplayerService.Instance.JoinSessionByCodeAsync(code, joinSessionOptions);
			_onSessionStarted.OnNext(ActiveSession);
			Debug.Log($"Joined session with code {code}");
			return ActiveSession;
		}

		public void SwitchScene(Scenes scene)
		{
			NetworkManager.Singleton.SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
		}

		public async UniTask QuickPlay()
		{
			await CheckForExistingSession();
			
			var quickJoinOptions = new QuickJoinOptions()
			                       {
				                       Filters = new List<FilterOption>
				                                 {
					                                 new(FilterField.AvailableSlots, "1", FilterOperation.Equal),
				                                 },
				                       Timeout = TimeSpan.FromSeconds(5)
			                       };
			
			var sessionOptions = new SessionOptions().WithRelayNetwork(); // createsession is false so it's useless but ctr demands it

			ActiveSession = await MultiplayerService.Instance.MatchmakeSessionAsync(quickJoinOptions, sessionOptions);
			_onSessionStarted.OnNext(ActiveSession);
		}

		public async UniTask CheckForExistingSession()
		{
			if (ActiveSession != null)
			{
				if (NetworkManager.Singleton.IsHost)
				{
					Debug.Log("Already hosting...");
					return;
				}

				await LeaveCurrentSession();
			}
		}

		public async UniTask LeaveCurrentSession()
		{
			if (ActiveSession != null)
			{
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
					       SessionStaticData.PLAYER_NAME_PROPERTY_KEY, playerNameProperty
				       }
			       };
		}
	}
}