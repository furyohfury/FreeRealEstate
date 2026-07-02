using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using R3;
using Unity.Netcode;
using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game
{
    public sealed class LobbySystem : IInitializable, IDisposable
    {
        public ISession Session { get; private set; }
        public Observable<LobbyEvent> OnLobbyEvent => _onLobbyEvent;

        private readonly Subject<LobbyEvent> _onLobbyEvent = new Subject<LobbyEvent>();
        private const string PLAYER_NAME_PROPERTY_KEY = "PLAYER_NAME_PROPERTY_KEY";
        private NetworkManager _networkManager;

        public LobbySystem(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public void Initialize()
        {
            _networkManager.OnClientConnectedCallback += OnClientConnectedCallback;
            _networkManager.OnClientDisconnectCallback += OnClientDisconnectCallback;
        }

        private void OnClientConnectedCallback(ulong id)
        {
            if (Session == null)
            {
                return;
            }

            if (_networkManager.IsServer)
            {
                _onLobbyEvent.OnNext(new LobbyEvent(LobbyEventType.Changed, Session));
            }
        }

        private async void OnClientDisconnectCallback(ulong id)
        {
            if (Session == null)
            {
                return;
            }

            if (_networkManager.IsServer)
            {
                _onLobbyEvent.OnNext(new LobbyEvent(LobbyEventType.Changed, Session));
            }
        }

        public async UniTask<ISession> HostPrivateSessionOrNull(string lobbyName, string playerNickname)
        {
            var playerProperties = GetPlayerProperties(playerNickname);
            var options = new SessionOptions
                          {
                              Name = lobbyName
                              , MaxPlayers = 2
                              , IsLocked = false
                              , IsPrivate = true
                              , PlayerProperties = playerProperties
                          }.WithRelayNetwork();

            try
            {
                IHostSession session = await MultiplayerService.Instance.CreateSessionAsync(options);
                Debug.Log($"Session {session.Id} created! Join code: {session.Code}");
                session.Changed += OnSessionChanged;
                session.Deleted += OnSessionDeleted;
                Session = session;
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
            _onLobbyEvent.OnNext(new LobbyEvent(LobbyEventType.Changed, Session));

            if (_networkManager.IsHost
                && Session.PlayerCount == Session.MaxPlayers)
            {
                _networkManager.SceneManager.LoadScene(ShooterScenes.SHOOTER2_X2_SCENE, LoadSceneMode.Single);
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
                Session = session;
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
            if (Session != null)
            {
                try
                {
                    Session.Changed -= OnSessionChanged;
                    Session.Deleted -= OnSessionDeleted;
                    
                    await Session.LeaveAsync();
                    
                    Session = null;
                    Debug.Log("Left session");
                    _onLobbyEvent.OnNext(new LobbyEvent(LobbyEventType.Leave, Session));
                }
                catch
                {
                }
            }
        }

        public string GetPlayerName(IReadOnlyPlayer player)
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
            throw new NotImplementedException();
        }
    }
}
