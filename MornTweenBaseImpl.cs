using System;
using MornEase;
using MornEditor;
using UnityEngine;

namespace MornLib._Morn.MornTween
{
    internal abstract class MornTweenBaseImpl<TValue> : MornTweenBase
    {
        [SerializeField] private bool _autoStart = true;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _delay;
        [SerializeField] protected bool HasStartValue;
        [SerializeField, ShowIf(nameof(HasStartValue))] private TValue _startValue;
        [SerializeField] protected bool EndToCurrent = true;
        [SerializeField, HideIf(nameof(EndToCurrent))] private TValue _endValue;
        [SerializeField] private bool _isRelative;
        [SerializeField] private MornEaseType _easeType;
        private float _cachedStartTime;
        private TValue _cachedStartValue;
        private TValue _cachedEndValue;
        public event Action OnComplete;
        public override float Progress => Mathf.Clamp01((Time.time - _cachedStartTime) / _duration);
        public bool IsRunning { get; private set; }

        private void Awake()
        {
            if (_autoStart)
            {
                TweenStart();
            }
        }

        public override void TweenStart()
        {
            if (IsRunning)
            {
                return;
            }

            IsRunning = true;
            _cachedStartTime = Time.time + _delay;
            if (HasStartValue)
            {
                _cachedStartValue = _isRelative ? GetRelativeValue(_startValue) : _startValue;
            }
            else
            {
                _cachedStartValue = GetValue();
            }

            if (EndToCurrent)
            {
                _cachedEndValue = GetValue();
            }
            else if (_isRelative)
            {
                _cachedEndValue = GetRelativeValue(_endValue);
            }
            
            SetValue(_cachedStartValue);
        }

        private void Update()
        {
            if (!IsRunning)
            {
                return;
            }

            var t = Mathf.Clamp01((Time.time - _cachedStartTime) / _duration);
            t = t.Ease(_easeType);
            var newValue = Lerp(_cachedStartValue, _cachedEndValue, t);
            SetValue(newValue);
            if (t >= 1)
            {
                OnComplete?.Invoke();
                OnComplete = null;
            }
        }

        protected abstract TValue GetValue();
        protected abstract TValue GetRelativeValue(TValue offset);
        protected abstract void SetValue(TValue value);
        protected abstract TValue Lerp(TValue start, TValue end, float t);
    }
}