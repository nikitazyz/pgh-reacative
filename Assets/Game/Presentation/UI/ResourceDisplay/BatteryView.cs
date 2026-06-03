using System;
using UnityEngine;
using UnityEngine.UI;

namespace Reacative.Presentation.UI
{
    public class BatteryView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        [SerializeField] private Sprite[] _batteryIcons;

        public void UpdateBattery(float batteryPercentage)
        {
            var step = (_batteryIcons.Length-1) * batteryPercentage;
            _icon.sprite = _batteryIcons[Mathf.CeilToInt(step)];
        }
    }
}