using System;
using R3;
using UIStackSystem;
using Unity.Services.Multiplayer;
using UnityEngine;

namespace Game
{
    public sealed class JoinSessionByCodePresenter : IPresenter
    {
        public ReactiveProperty<bool> IsCreateButtonInteractable { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> IsCreateInputFieldInteractable { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> IsCopyButtonInteractable { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<string> CopyInputFieldText { get; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> JoinInputFieldText { get; } = new ReactiveProperty<string>();
        public ReactiveProperty<bool> IsJoinButtonInteractable { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsJoinInputFieldInteractable { get; } = new ReactiveProperty<bool>(true);
        private string _createText;
        private string _joinText;

        private readonly LobbySystem _lobbySystem;
        private readonly PlayerProfile _playerProfile;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public JoinSessionByCodePresenter(LobbySystem lobbySystem, PlayerProfile playerProfile)
        {
            _lobbySystem = lobbySystem;
            _playerProfile = playerProfile;
        }

        public void Init()
        {
            _lobbySystem.OnLobbyEvent
                        .Subscribe(OnLobbyEvent)
                        .AddTo(_disposable);
        }

        public async void OnCreatePressed()
        {
            BlockEverything();

            ISession session = await _lobbySystem.HostPrivateSessionOrNull(_createText, _playerProfile.Nickname);

            if (session == null)
            {
                SetDefaultState();
                return;
            }

            ChangeUIWithActiveSession(session);
        }

        public void OnCopyPressed()
        {
            GUIUtility.systemCopyBuffer = CopyInputFieldText.Value ?? string.Empty;
        }

        public async void OnJoinPressed()
        {
            if (string.IsNullOrEmpty(_joinText))
                return;

            BlockEverything();

            var session = await _lobbySystem.JoinSessionByCodeOrNull(_joinText, _playerProfile.Nickname);

            if (session == null)
            {
                IsCreateButtonInteractable.Value = true;
                IsCreateInputFieldInteractable.Value = true;
                IsCopyButtonInteractable.Value = false;
                IsJoinButtonInteractable.Value = string.IsNullOrEmpty(_joinText) == false;
                IsJoinInputFieldInteractable.Value = true;
                return;
            }

            ChangeUIWithActiveSession(session);
        }

        public void OnCreateInputFieldChanged(string text)
        {
            IsCreateButtonInteractable.Value = string.IsNullOrEmpty(text) == false;
            _createText = text;
        }

        public void OnCopyInputFieldChanged(string text)
        {
            IsCopyButtonInteractable.Value = string.IsNullOrEmpty(text) == false;
        }

        public void OnJoinInputFieldChanged(string text)
        {
            _joinText = text;
            IsJoinButtonInteractable.Value = string.IsNullOrEmpty(text) == false;
        }

        private void OnLobbyEvent(LobbyEvent lobbyEvent)
        {
            switch (lobbyEvent.EventType)
            {
                case LobbyEventType.Create:
                case LobbyEventType.Join:
                case LobbyEventType.Changed:
                    ChangeUIWithActiveSession(lobbyEvent.Session);
                    break;
                case LobbyEventType.Leave:
                    CopyInputFieldText.Value = string.Empty;
                    IsCreateInputFieldInteractable.Value = true;
                    IsCreateButtonInteractable.Value = true;
                    IsJoinButtonInteractable.Value = string.IsNullOrEmpty(_joinText) == false;
                    IsJoinInputFieldInteractable.Value = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ChangeUIWithActiveSession(ISession session)
        {
            CopyInputFieldText.Value = session.Code;
            IsCreateInputFieldInteractable.Value = false;
            IsCreateButtonInteractable.Value = false;
            IsCopyButtonInteractable.Value = true;
            IsJoinButtonInteractable.Value = false;
            IsJoinInputFieldInteractable.Value = false;
        }

        private void BlockEverything()
        {
            IsCreateButtonInteractable.Value = false;
            IsCreateInputFieldInteractable.Value = false;
            IsCopyButtonInteractable.Value = false;
            IsJoinButtonInteractable.Value = false;
            IsJoinInputFieldInteractable.Value = false;
        }

        private void SetDefaultState()
        {
            IsCreateButtonInteractable.Value = true;
            IsCreateInputFieldInteractable.Value = true;
            IsCopyButtonInteractable.Value = false;
            IsJoinButtonInteractable.Value = false;
            JoinInputFieldText.Value = string.Empty;
            IsJoinInputFieldInteractable.Value = false;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
