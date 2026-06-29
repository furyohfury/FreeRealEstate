using System;
using Cysharp.Threading.Tasks;
using R3;
using TMPro;
using UIStackSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class EnterNicknameUI : Page<EnterNicknamePresenter>
    {
        [SerializeField]
        private TMP_InputField _nicknameInputField;
        [SerializeField]
        private Button _enterButton;

        private EnterNicknamePresenter _enterNicknamePresenter;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public override UniTask Open(IPresenter presenter, OpenPageOptions options)
        {
            if (presenter is not EnterNicknamePresenter enterNicknamePresenter)
            {
                throw new NullReferenceException("Presenter not found");
            }

            _enterNicknamePresenter = enterNicknamePresenter;
            _enterNicknamePresenter.IsEnterButtonInteractable
                                   .Subscribe(active => _enterButton.interactable = active)
                                   .AddTo(_disposable);
            _enterButton.OnClickAsObservable()
                        .Subscribe(_ => _enterNicknamePresenter.OnEnterButtonPressed())
                        .AddTo(_disposable);

            _nicknameInputField.OnValueChangedAsObservable()
                               .Subscribe(text => _enterNicknamePresenter.OnInputFieldValueChanged(text))
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
