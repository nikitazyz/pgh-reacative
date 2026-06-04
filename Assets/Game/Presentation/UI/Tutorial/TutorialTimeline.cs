using System;
using Reacative.Domain;
using Reacative.Infrastructure;
using Reacative.Infrastructure.Services;
using RRR;
using UnityEngine;

namespace Reacative.Presentation
{
    public class TutorialTimeline : MonoBehaviour
    {
        [SerializeField] private RRR_TutorialManager _reactorTutorial;
        [SerializeField] private RRR_TutorialManager _coolerTutorialp1;
        [SerializeField] private RRR_TutorialManager _coolerTutorialp2;
        [SerializeField] private RRR_TutorialManager _turbineTutorial;
        [SerializeField] private RRR_TutorialManager _catManagementTutorial;

        private Game _game;

        private void Start()
        {
            Debug.Log("[Tutorial] Init");
            _game = ServiceLocator.GetService<GameSession>().CurrentGame;
            _game.Subscribe(s => s.ResourceBankState.Energy, OnEnergyUpdate);
            _game.Subscribe(s => s.CoolerState.IsBought, OnBuyCooler);
            _game.Subscribe(s => s.TurbineState.IsBought, OnBuyTurbine);
            _game.Subscribe(s => s.SpecialistState.IsBought, OnBuySpecialist);
            
            _reactorTutorial.gameObject.SetActive(true);
            _reactorTutorial.StartTutorial();
        }

        private void OnBuySpecialist(bool obj)
        {
            if (!obj || _catManagementTutorial.WasPlayed)
            {
                return;
            }
            Debug.Log("[Tutorial] Buy specialist");
            _catManagementTutorial.gameObject.SetActive(true);
            _catManagementTutorial.StartTutorial();
        }

        private void OnBuyTurbine(bool obj)
        {
            if (!obj || _turbineTutorial.WasPlayed)
            {
                return;
            }
            Debug.Log("[Tutorial] Buy turbine");
            _turbineTutorial.gameObject.SetActive(true);
            _turbineTutorial.StartTutorial();
        }

        private void OnBuyCooler(bool obj)
        {
            if (!obj || _coolerTutorialp2.WasPlayed)
            {
                return;
            }
            Debug.Log("[Tutorial] Buy cooler");
            _coolerTutorialp2.gameObject.SetActive(true);
            _coolerTutorialp2.StartTutorial();
        }

        private void OnEnergyUpdate(double value)
        {
            if (value >= _game.Config.CoolerConfig.Cost && !_coolerTutorialp1.WasPlayed)
            {
                Debug.Log("[Tutorial] Cooler reached");
                _coolerTutorialp1.gameObject.SetActive(true);
                _coolerTutorialp1.StartTutorial();
            }
        }
    }

    
}
