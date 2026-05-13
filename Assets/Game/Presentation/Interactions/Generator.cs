using Reacative.Domain.CommandSystem;
using Reacative.Domain.State;
using Reacative.Infrastructure;
using Reacative.Infrastructure.Services;
using Reacative.Presentation.InteractionSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Reacative.Presentation.Interactions
{
    public class Generator : MonoBehaviour
    {
        [SerializeField] private InteractionReceiver _interactionReceiver;
        [SerializeField] private Image _progress;
        private GameSession _gameSession;

        public void Awake()
        {
            _gameSession = ServiceLocator.GetService<GameSession>();
            _interactionReceiver.OnInteract += OnInteract;

        }

        private void OnInteract(Interaction arg0)
        {
            var command = new GeneratorPowerCommand();
            _gameSession.CurrentGame.ExecuteCommand(command);
        }

        public void Start()
        {
            _gameSession.CurrentGame.OnStateChanged += OnStateChanged;
        }

        private void OnStateChanged(GameState oldState, GameState newState)
        {
            _progress.fillAmount = (float)_gameSession.CurrentGame.CurrentState.GeneratorState.Power;
        }
    }
}