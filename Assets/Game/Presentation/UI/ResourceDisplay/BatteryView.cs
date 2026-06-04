using System;
using UnityEngine;
using UnityEngine.UI;

namespace Reacative.Presentation.UI
{
    public class BatteryView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        [SerializeField] private Sprite[] _batteryIcons;
        [SerializeField] private RoundType _roundType = RoundType.Ceiling;

        public void UpdateBattery(float batteryPercentage)
        {
            var step = (_batteryIcons.Length-1) * batteryPercentage;
            var roundedStep = _roundType switch
            {
                RoundType.Floor => Mathf.FloorToInt(step),
                RoundType.Ceiling => Mathf.CeilToInt(step),
                RoundType.Round => Mathf.RoundToInt(step),
                _ => (int)step
            };
            _icon.sprite = _batteryIcons[roundedStep];
        }
    }

    internal enum RoundType
    {
        Floor,
        Ceiling,
        Round
    }
}