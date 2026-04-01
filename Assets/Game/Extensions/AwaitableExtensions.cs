using System.Threading;
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
}
