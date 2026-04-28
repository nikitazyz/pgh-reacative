using Reacative.Domain.CommandSystem;
using Reacative.Infrastructure;
using Reacative.Infrastructure.Services;
using Reacative.Presentation.InteractionSystem;
using UnityEngine;

namespace Reacative.Presentation.Interactions
{
    public class Turbine : MonoBehaviour
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

            var turbineCommand = new StartTurbineCommand();
            _gameSession.CurrentGame.ExecuteCommand(turbineCommand);
        }
    }
}