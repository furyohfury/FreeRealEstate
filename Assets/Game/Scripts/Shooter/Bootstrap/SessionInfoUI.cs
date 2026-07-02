using Cysharp.Threading.Tasks;
using R3;
using TMPro;
using UIStackSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class SessionInfoUI : Page<SessionInfoPresenter>
    {
        [SerializeField]
        private Button _leaveButton;
        [SerializeField]
        private TMP_Text _lobbyName;
        [SerializeField]
        private TMP_Text _numberOfPlayers;
        [SerializeField]
        private RectTransform _playerNicknamesContainer;
        [SerializeField]
        private PlayerNicknameUI _playerNicknameUIPrefab;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private SessionInfoPresenter _presenter;

        public override UniTask Open(IPresenter presenter, OpenPageOptions options)
        {
            if (presenter is not SessionInfoPresenter sessionInfoPresenter)
            {
                Debug.LogError("SessionInfoPresenter not found");
                return UniTask.CompletedTask;
            }

            foreach (Transform child in _playerNicknamesContainer)
            {
                Destroy(child.gameObject);
            }

            _presenter = sessionInfoPresenter;

            _leaveButton.OnClickAsObservable()
                        .Subscribe(_ => _presenter.OnLeavePressed())
                        .AddTo(_disposable);

            _presenter.IsLeaveButtonInteractable
                      .Subscribe(v => _leaveButton.interactable = v)
                      .AddTo(_disposable);

            _presenter.LobbyName
                      .Subscribe(v => _lobbyName.text = v)
                      .AddTo(_disposable);

            _presenter.NumberOfPlayers
                      .Subscribe(v => _numberOfPlayers.text = v)
                      .AddTo(_disposable);

            _presenter.PlayerNicknames
                      .Subscribe(nicknames =>
                      {
                          foreach (Transform child in _playerNicknamesContainer)
                          {
                              Destroy(child.gameObject);
                          }

                          for (int i = 0, count = nicknames.Length; i < count; i++)
                          {
                              PlayerNicknameUI playerNicknameUI = Instantiate(_playerNicknameUIPrefab, _playerNicknamesContainer);
                              playerNicknameUI.SetNickname(nicknames[i]);
                          }
                      })
                      .AddTo(_disposable);

            return base.Open(presenter, options);
        }

        public override UniTask Close(ClosePageOptions options)
        {
            _disposable.Dispose();
            return base.Close(options);
        }
    }
}
