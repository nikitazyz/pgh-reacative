using System;
using UnityEngine;

namespace Reacative.Presentation
{
    public class ClockBar : MonoBehaviour
    {
        [SerializeField] private Transform _arrow;
        [SerializeField] private float _lowAngle;
        [SerializeField] private float _highAngle;

        [SerializeField, Range(0, 1)] private float _value;

        public float Value
        {
            get => _value;
            set => UpdateValue(value);
        }

        private void UpdateValue(float value)
        {
            _value = Mathf.Clamp01(value);
            _arrow.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(_lowAngle, _highAngle, value));
        }
    }
}
