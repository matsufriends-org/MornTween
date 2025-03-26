using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MornEase;
using UnityEngine;

namespace MornLib
{
    internal sealed class MornTween<T> : IMornTween
    {
        private CancellationTokenSource _cts;
        private readonly UniTask _cachedTask;

        internal MornTween(Func<T> getter, Action<T> setter, Func<T, T, float, T> lerpFunc, T value, float duration,
            MornEaseType easeType = MornEaseType.EaseOutQuad, CancellationToken ct = default)
        {
            _cachedTask = DOAsync(getter, setter, lerpFunc, value, duration, easeType, ct);
        }

        private async UniTask DOAsync(Func<T> getter, Action<T> setter, Func<T, T, float, T> lerpFunc, T value,
            float duration, MornEaseType easeType, CancellationToken ct)
        {
            _cts?.Cancel();
            _cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            var start = getter();
            var end = value;
            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                var rate = elapsedTime / duration;
                var lerpT = rate.Ease(easeType);
                setter(lerpFunc(start, end, lerpT));
                elapsedTime += Time.deltaTime;
                await UniTask.Yield(_cts.Token);
            }

            if (!_cts.Token.IsCancellationRequested)
            {
                setter(end);
            }

            _cts.Dispose();
            _cts = null;
        }

        UniTask IMornTween.GetAwaiter()
        {
            return _cachedTask;
        }

        void IDisposable.Dispose()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }
    }
}