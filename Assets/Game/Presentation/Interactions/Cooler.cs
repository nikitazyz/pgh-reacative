using Reacative.Domain.CommandSystem;
using Reacative.Infrastructure;
using Reacative.Infrastructure.Services;
using Reacative.Presentation.InteractionSystem;
using UnityEngine;

namespace Reacative.Presentation.Interactions
{
    public class Cooler : MonoBehaviour
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
            var coolCommand = new CoolCommand();
            
            _gameSession.CurrentGame.ExecuteCommand(coolCommand);
        }
    }
}
