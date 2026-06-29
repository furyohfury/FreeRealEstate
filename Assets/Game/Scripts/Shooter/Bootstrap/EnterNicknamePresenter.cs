using R3;
using UIStackSystem;

namespace Game
{
    public sealed class EnterNicknamePresenter : IPresenter
    {
        public ReactiveProperty<bool> IsEnterButtonInteractable { get; } = new ReactiveProperty<bool>(false);

        private readonly PlayerProfile _playerProfile;
        private string _nicknameField;

        public EnterNicknamePresenter(PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
        }

        public void OnInputFieldValueChanged(string text)
        {
            IsEnterButtonInteractable.Value = string.IsNullOrEmpty(text) == false;
            _nicknameField = text;
        }

        public void OnEnterButtonPressed()
        {
            _playerProfile.SetNickname(_nicknameField);
        }

        public void Init()
        {
        }

        public void Dispose()
        {
        }
    }
}
