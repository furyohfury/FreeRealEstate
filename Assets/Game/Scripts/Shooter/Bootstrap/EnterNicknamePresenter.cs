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
            var sessionInfoOpenOptions = _uiManager.CreateOpenPageOptions<SessionInfoPresenter>().WithPosition(new Vector2(canvasSize.x * -0.25f, 0));
            _uiManager.OpenPage<SessionInfoPresenter>(sessionInfoOpenOptions).Forget();
            var joinSessionByCodeOpenOptions = _uiManager.CreateOpenPageOptions<SessionInfoPresenter>().WithPosition(new Vector2(canvasSize.x * 0.25f, 0));
            _uiManager.OpenPage<JoinSessionByCodePresenter>(joinSessionByCodeOpenOptions).Forget();
        }

        public void Init()
        {
        }

        public void Dispose()
        {
        }
    }
}
