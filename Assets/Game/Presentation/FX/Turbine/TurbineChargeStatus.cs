using System;
using Reacative.Domain;
using Reacative.Infrastructure;
using Reacative.Infrastructure.Services;
using Reacative.Presentation.UI;
using UnityEngine;

namespace Reacative.Presentation
{
    public class TurbineChargeStatus : MonoBehaviour
    {
        [SerializeField] private BatteryView _batteryView;

        private Game _game;

        private void Start()
        {
            _game = ServiceLocator.GetService<GameSession>().CurrentGame;
        }

        private void FixedUpdate()
        {
            _batteryView.UpdateBattery((float)_game.GetTurbineReloadProgress());
        }
    }
}
