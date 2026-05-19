using Reacative.Infrastructure.InteractionSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Reacative.Presentation.Interactions
{
    public class InteractableOutline : MonoBehaviour
    {
        [SerializeField] private InteractionReceiver _interactionReceiver;
        [SerializeField] private Outline _outline;

        private void Awake()
        {
            _interactionReceiver.OnInteractEnter += InteractEnter;
            _interactionReceiver.OnInteractExit += InteractExit;
            _outline.enabled = false;
        }

        public void InteractExit()
        {
            _outline.enabled = false;
        }

        public void InteractEnter()
        {
            _outline.enabled = true;
        }
    }
}