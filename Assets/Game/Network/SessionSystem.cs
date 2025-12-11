using System;
using System.Collections.Generic;
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
		public ReadOnlyReactiveProperty<ISession> ActiveSession => activeSession;
		public bool IsLeaving {get; private set;}

		private readonly ReactiveProperty<ISession> activeSession = new ReactiveProperty<ISession>();
		private readonly Subject<ISession> _onSessionStarted = new Subject<ISession>();

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
			var options = new SessionOptions
			              {
				              MaxPlayers = 2
				              , IsLocked = false
				              , IsPrivate = true
				              , PlayerProperties = playerProperties
			              }.WithRelayNetwork();

			activeSession.Value = await MultiplayerService.Instance.CreateSessionAsync(options);
			_onSessionStarted.OnNext(ActiveSession.CurrentValue);
			Debug.Log($"Session {ActiveSession.CurrentValue.Id} created! Join code: {ActiveSession.CurrentValue.Code}");
		}

		public async UniTask<ISession> HostPublicSession(string playerNickname)
		{
			await CheckForExistingSession();

			var playerProperties = GetPlayerProperties(playerNickname);
			var options = new SessionOptions
			              {
				              MaxPlayers = 2
				              , IsLocked = false
				              , IsPrivate = false
				              , PlayerProperties = playerProperties
			              }.WithRelayNetwork();

			activeSession.Value = await MultiplayerService.Instance.CreateSessionAsync(options);
			_onSessionStarted.OnNext(ActiveSession.CurrentValue);
			Debug.Log($"Public session {ActiveSession.CurrentValue.Id} created!");
			return ActiveSession.CurrentValue;
		}

		public async UniTask<ISession> JoinSessionByCode(string code, string nickname)
		{
			var joinSessionOptions = new JoinSessionOptions
			                         {
				                         PlayerProperties = GetPlayerProperties(nickname)
			                         };

			activeSession.Value = await MultiplayerService.Instance.JoinSessionByCodeAsync(code, joinSessionOptions);
			_onSessionStarted.OnNext(ActiveSession.CurrentValue);
			Debug.Log($"Joined session with code {code}");
			return ActiveSession.CurrentValue;
		}

		public void SwitchScene(Scenes scene)
		{
			NetworkManager.Singleton.SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
		}

		public async UniTask QuickPlay()
		{
			await CheckForExistingSession();

			var quickJoinOptions = new QuickJoinOptions
			                       {
				                       Filters = new List<FilterOption>
				                                 {
					                                 new FilterOption(FilterField.AvailableSlots, "1", FilterOperation.Equal)
				                                 }
				                       , Timeout = TimeSpan.FromSeconds(5)
			                       };

			var sessionOptions = new SessionOptions().WithRelayNetwork(); // createsession is false so it's useless but ctr demands it

			activeSession.Value = await MultiplayerService.Instance.MatchmakeSessionAsync(quickJoinOptions, sessionOptions);
			_onSessionStarted.OnNext(ActiveSession.CurrentValue);
		}

		public async UniTask CheckForExistingSession()
		{
			if (ActiveSession.CurrentValue != null)
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
			if (ActiveSession.CurrentValue != null)
			{
				IsLeaving = true;
				await ActiveSession.CurrentValue.LeaveAsync();
				activeSession.Value = null;
				IsLeaving = false;
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
