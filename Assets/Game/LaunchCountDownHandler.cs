using System.Threading;
using UnityEngine;

namespace Game
{
    public sealed class LaunchCountDownHandler : MonoBehaviour
    {
        public float Countdown { get; private set; }
        [SerializeField]
        private int _initialCountdown = 3;

        public async Awaitable CountdownAsync(CancellationToken cancellationToken = default)
        {
            Countdown = _initialCountdown;
            CountDownUI.Instance.LaunchCountdown(_initialCountdown);

            while (Countdown >= 0)
            {
                Countdown -= Time.deltaTime;
                await Awaitable.NextFrameAsync(cancellationToken);
            }
        }
    }
}
