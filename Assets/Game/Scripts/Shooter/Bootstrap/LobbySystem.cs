using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using R3;
using Unity.Netcode;
using Unity.Services.Multiplayer;
using UnityEngine;

namespace Game
{
    public sealed class LobbySystem : IDisposable
    {
        public SessionInfo SessionInfo { get; private set; }
        public Observable<LobbyEvent> OnLobbyEvent => _onLobbyEvent;

        private readonly Subject<LobbyEvent> _onLobbyEvent = new Subject<LobbyEvent>();
        private const string PLAYER_NAME_PROPERTY_KEY = "PLAYER_NAME_PROPERTY_KEY";
        private readonly NetworkManager _networkManager;
        private readonly LobbyGameplayLauncher _lobbyGameplayLauncher;

        public LobbySystem(NetworkManager networkManager, LobbyGameplayLauncher lobbyGameplayLauncher)
        {
            _networkManager = networkManager;
            _lobbyGameplayLauncher = lobbyGameplayLauncher;
        }

        public async UniTask<ISession> HostPrivateSessionOrNull(string lobbyName, string playerNickname)
        {
            var playerProperties = GetPlayerProperties(playerNickname);
            var options = new SessionOptions
                          {
                              Name = lobbyName,
                              MaxPlayers = 2,
                              IsLocked = false,
                              IsPrivate = true,
                              PlayerProperties = playerProperties
                          }.WithRelayNetwork();

            try
            {
                IHostSession session = await MultiplayerService.Instance.CreateSessionAsync(options);
                Debug.Log($"Session {session.Id} created! Join code: {session.Code}");
                session.Changed += OnSessionChanged;
                session.Deleted += OnSessionDeleted;
                SessionInfo = new SessionInfo(session, new LobbyPlayerInfo(session.Host, playerNickname));
                _onLobbyEvent.OnNext(new LobbyEvent(LobbyEventType.Create, session));

                return session;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }

        private void OnSessionChanged()
        {
            ISession session = SessionInfo.Session;
            _onLobbyEvent.OnNext(new LobbyEvent(LobbyEventType.Changed, session));

            if (_networkManager.IsHost && session.PlayerCount == session.MaxPlayers)
            {
                _lobbyGameplayLauncher.LaunchGame(SessionInfo).Forget();
            }
        }

        private async void OnSessionDeleted()
        {
            await LeaveCurrentSession();
        }

        public async UniTask<ISession> JoinSessionByCodeOrNull(string code, string nickname)
        {
            var joinSessionOptions = new JoinSessionOptions
                                     {
                                         PlayerProperties = GetPlayerProperties(nickname)
                                     };

            try
            {
                ISession session = await MultiplayerService.Instance.JoinSessionByCodeAsync(code, joinSessionOptions);

                Debug.Log($"Joined session with code {code}");
                session.Changed += OnSessionChanged;
                session.Deleted += OnSessionDeleted;
                IReadOnlyList<IReadOnlyPlayer> players = session.Players;

                var lobbyPlayerInfos = new LobbyPlayerInfo[players.Count];

                for (int i = 0, count = lobbyPlayerInfos.Length; i < count; i++)
                {
                    lobbyPlayerInfos[i] = new LobbyPlayerInfo(players[i].Id, GetPlayerNickname(players[i]));
                }

                SessionInfo = new SessionInfo(session, lobbyPlayerInfos);
                _onLobbyEvent.OnNext(new LobbyEvent(LobbyEventType.Join, session));

                return session;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }

        public async UniTask LeaveCurrentSession()
        {
            if (SessionInfo != null)
            {
                try
                {
                    ISession session = SessionInfo.Session;
                    session.Changed -= OnSessionChanged;
                    session.Deleted -= OnSessionDeleted;

                    await session.LeaveAsync();

                    _onLobbyEvent.OnNext(new LobbyEvent(LobbyEventType.Leave, session));
                    SessionInfo = null;
                    Debug.Log("Left session");
                }
                catch
                {
                }
            }
        }

        public string GetPlayerNickname(IReadOnlyPlayer player)
        {
            return player.Properties.TryGetValue(PLAYER_NAME_PROPERTY_KEY, out var playerNameProperty)
                ? playerNameProperty.Value
                : string.Empty;
        }

        private Dictionary<string, PlayerProperty> GetPlayerProperties(string playerNickname)
        {
            var playerNameProperty = new PlayerProperty(playerNickname, VisibilityPropertyOptions.Member);
            return new Dictionary<string, PlayerProperty>
                   {
                       {
                           PLAYER_NAME_PROPERTY_KEY, playerNameProperty
                       }
                   };
        }

        public void Dispose()
        {
            if (SessionInfo != null)
            {
                ISession session = SessionInfo.Session;
                session.Changed -= OnSessionChanged;
                session.Deleted -= OnSessionDeleted;
            }
        }
    }
}
