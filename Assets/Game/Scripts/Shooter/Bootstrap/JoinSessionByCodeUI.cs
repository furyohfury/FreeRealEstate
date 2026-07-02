using Cysharp.Threading.Tasks;
using R3;
using TMPro;
using UIStackSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class JoinSessionByCodeUI : Page<JoinSessionByCodePresenter>
    {
        [SerializeField]
        private TMP_InputField _createInputField;
        [SerializeField]
        private TMP_InputField _copyInputField;
        [SerializeField]
        private TMP_InputField _joinInputField;
        [SerializeField]
        private Button _createButton;
        [SerializeField]
        private Button _copyButton;
        [SerializeField]
        private Button _joinButton;

        private JoinSessionByCodePresenter _presenter;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public override UniTask Open(IPresenter presenter, OpenPageOptions options)
        {
            if (presenter is not JoinSessionByCodePresenter joinSessionByCodePresenter)
            {
                return UniTask.CompletedTask;
            }

            _presenter = joinSessionByCodePresenter;

            _createButton.OnClickAsObservable()
                         .Subscribe(_ => _presenter.OnCreatePressed())
                         .AddTo(_disposable);

            _copyButton.OnClickAsObservable()
                       .Subscribe(_ => _presenter.OnCopyPressed())
                       .AddTo(_disposable);

            _joinButton.OnClickAsObservable()
                       .Subscribe(_ => _presenter.OnJoinPressed())
                       .AddTo(_disposable);

            _presenter.IsCreateButtonInteractable
                      .Subscribe(v => _createButton.interactable = v)
                      .AddTo(_disposable);

            _presenter.IsCopyButtonInteractable
                      .Subscribe(v => _copyButton.interactable = v)
                      .AddTo(_disposable);

            _presenter.IsJoinButtonInteractable
                      .Subscribe(v => _joinButton.interactable = v)
                      .AddTo(_disposable);

            _presenter.IsCreateInputFieldInteractable
                      .Subscribe(v => _createInputField.interactable = v)
                      .AddTo(_disposable);
            
            _presenter.CopyInputFieldText
                      .Subscribe(text => _copyInputField.text = text)
                      .AddTo(_disposable);
            
            _presenter.JoinInputFieldText
                      .Subscribe(text => _joinInputField.text = text)
                      .AddTo(_disposable);

            _presenter.IsJoinInputFieldInteractable
                      .Subscribe(v => _joinInputField.interactable = v)
                      .AddTo(_disposable);

            _createInputField.OnValueChangedAsObservable()
                             .Subscribe(text => _presenter.OnCreateInputFieldChanged(text))
                             .AddTo(_disposable);
            
            _copyInputField.OnValueChangedAsObservable()
                           .Subscribe(text => _presenter.OnCopyInputFieldChanged(text))
                           .AddTo(_disposable);
            
            _joinInputField.OnValueChangedAsObservable()
                           .Subscribe(text => _presenter.OnJoinInputFieldChanged(text))
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
