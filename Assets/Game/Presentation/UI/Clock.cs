using System;
using TMPro;
using UnityEngine;

namespace Reacative.Presentation.UI
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        private float _lastUpdate;
        private readonly float _updateInterval = 0.5f;
        private void Update()
        {
            if (_lastUpdate + _updateInterval > Time.time)
            {
                return;
            }
            
            _lastUpdate = Time.time;
            var date = DateTime.Now;
            _text.text = date.ToString("HH:mm");
        }
    }
}
