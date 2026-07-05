using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using R3;
using UIStackSystem;
using Unity.Services.Multiplayer;

namespace Game
{
    public sealed class SessionInfoPresenter : IPresenter
    {
        public ReactiveProperty<string> LobbyName { get; } = new ReactiveProperty<string>("lobby name");
        public ReactiveProperty<string> NumberOfPlayers { get; } = new ReactiveProperty<string>("0/n");
        public ReactiveProperty<bool> IsLeaveButtonInteractable { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<string[]> PlayerNicknames { get; } = new ReactiveProperty<string[]>(Array.Empty<string>());

        private readonly LobbySystem _lobbySystem;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public SessionInfoPresenter(LobbySystem lobbySystem)
        {
            _lobbySystem = lobbySystem;
        }

        public void Init()
        {
            _lobbySystem.OnLobbyEvent
                        .Subscribe(ev =>
                        {
                            ISession session = ev.Session;

                            switch (ev.EventType)
                            {
                                case LobbyEventType.Create:
                                case LobbyEventType.Join:
                                case LobbyEventType.Changed:
                                    OnHaveActiveLobby(session);
                                    break;
                                case LobbyEventType.Leave:
                                    OnLeftLobby();
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        })
                        .AddTo(_disposable);
        }

        public async UniTask OnLeavePressed()
        {
            IsLeaveButtonInteractable.Value = false;

            await _lobbySystem.LeaveCurrentSession();

            OnLeftLobby();
        }

        private void OnHaveActiveLobby(ISession session)
        {
            LobbyName.Value = session.Name;
            NumberOfPlayers.Value = $"{session.PlayerCount}/{session.MaxPlayers}";
            IsLeaveButtonInteractable.Value = true;
            PlayerNicknames.Value = session.Players
                                           .Select(player => _lobbySystem.GetPlayerNickname(player))
                                           .ToArray();
        }

        private void OnLeftLobby()
        {
            IsLeaveButtonInteractable.Value = false;
            LobbyName.Value = "lobby name";
            NumberOfPlayers.Value = "0/n";
            PlayerNicknames.Value = Array.Empty<string>();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
