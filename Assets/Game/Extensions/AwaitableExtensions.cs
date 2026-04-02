using System.Threading;
using DG.Tweening;
using UnityEngine;

public static class AwaitableExtensions
{
    public static async Awaitable WaitForSecondsRealtimeAsync(float seconds, CancellationToken ct = default)
    {
        float startTime = Time.unscaledTime;

        while (Time.unscaledTime - startTime < seconds)
        {
            await Awaitable.NextFrameAsync(ct);
        }
    }

    public static async Awaitable WaitForTweenRealtime(Tween tween, CancellationToken ct = default)
    {
        float duration = tween.Duration();

        await WaitForSecondsRealtimeAsync(duration, ct);
    }
}
