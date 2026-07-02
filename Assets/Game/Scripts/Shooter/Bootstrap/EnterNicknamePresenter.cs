using Cysharp.Threading.Tasks;
using R3;
using UIStackSystem;
using UnityEngine;

namespace Game
{
    public sealed class EnterNicknamePresenter : IPresenter
    {
        public ReactiveProperty<bool> IsEnterButtonInteractable { get; } = new ReactiveProperty<bool>(false);

        private readonly PlayerProfile _playerProfile;
        private readonly UIManager _uiManager;
        private string _nicknameField;

        public EnterNicknamePresenter(PlayerProfile playerProfile, UIManager uiManager)
        {
            _playerProfile = playerProfile;
            _uiManager = uiManager;
        }

        public void OnInputFieldValueChanged(string text)
        {
            IsEnterButtonInteractable.Value = string.IsNullOrEmpty(text) == false;
            _nicknameField = text;
        }

        public async void OnEnterButtonPressed()
        {
            _playerProfile.SetNickname(_nicknameField);

            await _uiManager.CloseTop();

            Vector2 canvasSize = _uiManager.GetCanvasSize();
            _uiManager.OpenPage<SessionInfoPresenter>(OpenPageOptions.Create()
                                                                     .WithPosition(new Vector2(canvasSize.x * -0.25f, 0)))
                      .Forget();
            _uiManager.OpenPage<JoinSessionByCodePresenter>(OpenPageOptions.Create()
                                                                           .WithPosition(new Vector2(canvasSize.x * 0.25f, 0)))
                      .Forget();
        }

        public void Init()
        {
        }

        public void Dispose()
        {
        }
    }
}
