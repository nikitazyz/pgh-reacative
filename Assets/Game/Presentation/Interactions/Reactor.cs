using Reacative.Domain.CommandSystem;
using Reacative.Infrastructure;
using Reacative.Infrastructure.Services;
using Reacative.Presentation.InteractionSystem;
using UnityEngine;

namespace Reacative.Presentation.Interactions
{
    public class Reactor : MonoBehaviour
    {
        [SerializeField] private InteractionReceiver _interactionReceiver;
        private GameSession _gameSession;
        private void Awake()
        {
            _gameSession = ServiceLocator.GetService<GameSession>();
            _interactionReceiver.OnInteract += Interact;
        }

        public void Interact(Interaction interaction)
        {
            if (!interaction.LeftMouseButton)
            {
                return;
            }

            var command = new ReactorActivateCommand(!_gameSession.CurrentGame.CurrentState.ReactorState.IsActive);
            _gameSession.CurrentGame.ExecuteCommand(command);
            
            var isActive = _gameSession.CurrentGame.CurrentState.ReactorState.IsActive;
            _interactionReceiver.InteractionPrompt = isActive ? "Off" : "On";
        }
    }
}