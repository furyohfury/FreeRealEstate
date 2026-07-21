using System;
using TriInspector;
using UIStackSystem;
using UnityEngine;
using Zenject;

namespace Game
{
    public sealed class LobbyUIDebugHelper : MonoBehaviour
    {
        [Inject]
        private UIManager _uiManager;

        [Button]
        public async void OpenUI()
        {
            // Vector2 canvasSize = _uiManager.GetCanvasSize();
            // await _uiManager.OpenPage<JoinSessionByCodePresenter>(OpenPageOptions.Create()
            //                                                                      .WithPosition(new Vector2(canvasSize.x * -0.25f, 0)));
            // await _uiManager.OpenPage<SessionInfoPresenter>(OpenPageOptions.Create()
            //                                                                .WithPosition(new Vector2(canvasSize.x* 0.25f,0)));
        }
    }
}
