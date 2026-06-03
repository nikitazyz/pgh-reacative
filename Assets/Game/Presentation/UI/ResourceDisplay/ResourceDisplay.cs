using System;
using Reacative.Domain;
using Reacative.Infrastructure;
using Reacative.Infrastructure.Services;
using Reacative.Infrastructure.UI.ResourceDisplay;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

namespace Reacative.Presentation.UI
{
    public class ResourceDisplay : MonoBehaviour, IResourceDisplayView
    {
        [SerializeField] private WalletView _energyView;
        [SerializeField] private ClockBar _temperatureView;
        [SerializeField] private BatteryView _batteryView;
        private Game _game;

        private void Awake()
        {
            _game = ServiceLocator.GetService<GameSession>().CurrentGame;
        }

        public void UpdateResources(double energy, double temperature, double power)
        {
            _energyView.UpdateText((int)energy);
            var maxTemp = _game.GetMaxTemperature();
            Debug.Log(maxTemp);
            Debug.Log(temperature);
            _temperatureView.Value = (float)(temperature / maxTemp);
            _batteryView.UpdateBattery((float)power);
        }

        public bool IsActive => gameObject.activeSelf;

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}