using System;
using System.Threading;
using MornEase;
using UnityEngine;

namespace MornLib
{
    public static class MornTweenUtil
    {
        private static IMornTween To<T>(Func<T> getter, Action<T> setter, Func<T, T, float, T> lerpFunc, T value,
            float duration, MornEaseType easeType = MornEaseType.EaseOutQuad, CancellationToken ct = default)
        {
            var tween = new MornTween<T>(getter, setter, lerpFunc, value, duration, easeType, ct);
            return tween;
        }

        public static IMornTween To(Func<float> getter, Action<float> setter, float value, float duration,
            MornEaseType easeType = MornEaseType.EaseOutQuad, CancellationToken ct = default)
        {
            return To(getter, setter, Mathf.Lerp, value, duration, easeType, ct);
        }

        public static IMornTween DOLocalMove(this Transform transform, Vector3 value, float duration,
            MornEaseType easeType = MornEaseType.EaseOutQuad, CancellationToken ct = default)
        {
            return To(
                () => transform.localPosition,
                v => transform.localPosition = v,
                Vector3.Lerp,
                value,
                duration,
                easeType,
                ct);
        }

        public static IMornTween DOFade(this SpriteRenderer spriteRenderer, float value, float duration,
            MornEaseType easeType = MornEaseType.EaseOutQuad, CancellationToken ct = default)
        {
            return To(
                () => spriteRenderer.color.a,
                v => spriteRenderer.color = new Color(
                    spriteRenderer.color.r,
                    spriteRenderer.color.g,
                    spriteRenderer.color.b,
                    v),
                Mathf.Lerp,
                value,
                duration,
                easeType,
                ct);
        }
    }
}