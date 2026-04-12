using System;
using System.Threading;
using UnityEngine;
using YG;
using YG.Utils.LB;

namespace Game.Application.Leaderboard
{
    public sealed class YGLeaderboardProvider : Singleton<YGLeaderboardProvider>
    {
        private AwaitableCompletionSource<LBData> _completionSource;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            YG2.onGetLeaderboard += OnGetLeaderboard;
        }

        public async Awaitable<LBData> GetLeaderboard(string lbName, CancellationToken token)
        {
            // Инициализируем источник результата
            _completionSource = new AwaitableCompletionSource<LBData>();

            // Вызываем метод SDK
            YG2.GetLeaderboard(lbName);

            // Регистрируем колбэк отмены. 
            // Если токен отменится (по таймауту или вручную), мы прервем ожидания источника.
            await using (token.Register(() => _completionSource.TrySetCanceled()))
            {
                try
                {
                    return await _completionSource.Awaitable;
                }
                catch (OperationCanceledException)
                {
                    // Здесь можно логировать отмену или прокинуть её дальше
                    Debug.LogWarning($"Запрос к лидерборду {lbName} был отменен.");
                    throw;
                }
            }
        }

        private void OnGetLeaderboard(LBData data)
        {
            _completionSource?.TrySetResult(data);
        }

        private void OnDestroy()
        {
            YG2.onGetLeaderboard -= OnGetLeaderboard;
        }
    }
}
