using UnityEngine;

namespace MornLib._Morn.MornTween
{
    public abstract class MornTweenBase : MonoBehaviour
    {
        public abstract float Progress { get; }
        public abstract void TweenStart();
    }
}