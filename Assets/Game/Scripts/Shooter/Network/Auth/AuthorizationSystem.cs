using System;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace Game.Auth
{
    public sealed class AuthorizationSystem
    {
        public string PlayerId { get; private set; }
        public bool IsAuthorized { get; private set; }
        private UniTaskCompletionSource _activeAuthTask;

        public async UniTask Authorize()
        {
            if (_activeAuthTask != null)
            {
                await _activeAuthTask.Task;
                return;
            }

            try
            {
                _activeAuthTask = new UniTaskCompletionSource();
                await UnityServices.InitializeAsync().AsUniTask();
                await AuthenticationService.Instance.SignInAnonymouslyAsync().AsUniTask();
                PlayerId = AuthenticationService.Instance.PlayerId;
                IsAuthorized = true;
                Debug.Log($"Sign in anonymously succeeded! PlayerID: {PlayerId}");
                _activeAuthTask.TrySetResult();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                _activeAuthTask?.TrySetException(e);
            }
        }
    }
}
