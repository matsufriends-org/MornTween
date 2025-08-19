using MornLib._Morn.MornTween;
using UnityEngine;

namespace MornArbor.Tween
{
    internal sealed class MornTweenMove : MornTweenBaseImpl<Vector3>
    {
        [SerializeField] private Transform _target;

        protected override Vector3 GetValue()
        {
            return _target.position;
        }

        protected override Vector3 GetRelativeValue(Vector3 offset)
        {
            return _target.position + offset;
        }

        protected override void SetValue(Vector3 value)
        {
            _target.position = value;
        }

        protected override Vector3 Lerp(Vector3 start, Vector3 end, float t)
        {
            return Vector3.Lerp(start, end, t);
        }
    }
}