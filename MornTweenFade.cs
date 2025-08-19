using MornLib._Morn.MornTween;
using UnityEngine;
using UnityEngine.UI;

namespace MornArbor.Tween
{
    internal sealed class MornTweenFade : MornTweenBaseImpl<float>
    {
        [SerializeField] private Image _target;

        protected override float GetValue()
        {
            return _target.color.a;
        }

        protected override float GetRelativeValue(float offset)
        {
            var color = _target.color;
            return Mathf.Clamp01(color.a + offset);
        }

        protected override void SetValue(float value)
        {
            var color = _target.color;
            color.a = value;
            _target.color = color;
        }

        protected override float Lerp(float start, float end, float t)
        {
            return Mathf.Lerp(start, end, t);
        }
    }
}