using Reacative.Domain;
using Reacative.Infrastructure;
using Reacative.Infrastructure.Services;
using UnityEngine;

namespace Reacative.Presentation.FX
{
    public class ReactorAnimation : MonoBehaviour
    {
        private static readonly int IsActive = Animator.StringToHash("IsActive");
        private static readonly int Temperature = Animator.StringToHash("TemperatureMultiplier");
        [SerializeField] private Animator _animator;
        private Game _game;

        private void Start()
        {
            _game = ServiceLocator.GetService<GameSession>().CurrentGame;
            _game.Subscribe(state => state.ReactorState.IsActive, isActive => _animator.SetBool(IsActive, isActive));
            _game.Subscribe(state => state.ReactorState.Temperature, TemperatureChange);
        }

        private void TemperatureChange(double temperature)
        {
            var maxTemp = _game.GetMaxTemperature();
            var percent = temperature / maxTemp;
            
            _animator.SetFloat(Temperature, (float)percent*2 + 1f);
        }
    }
}
