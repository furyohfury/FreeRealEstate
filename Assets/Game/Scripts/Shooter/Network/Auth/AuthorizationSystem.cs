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

        public async UniTask Authorize()
        {
            try
            {
                await UnityServices.InitializeAsync();
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                PlayerId = AuthenticationService.Instance.PlayerId;
                IsAuthorized = true;
                Debug.Log($"Sign in anonymously succeeded! PlayerID: {PlayerId}");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
