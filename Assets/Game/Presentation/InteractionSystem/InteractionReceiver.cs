using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Reacative.Presentation.InteractionSystem
{
    public class InteractionReceiver : MonoBehaviour, IInteractableEnter, IInteractableExit
    {
        [field: SerializeField]
        public string InteractionPrompt { get; set; }
        
        [SerializeField]
        private UnityEvent _onInteractEnter;
        [SerializeField]
        private UnityEvent _onInteractExit;
        [SerializeField]
        private UnityEvent<Interaction> _onInteract;
        
        public event UnityAction OnInteractEnter
        {
            add => _onInteractEnter.AddListener(value);
            remove => _onInteractEnter.RemoveListener(value);
        }
        public event UnityAction OnInteractExit
        {
            add => _onInteractExit.AddListener(value);
            remove => _onInteractExit.RemoveListener(value);
        }
        public event UnityAction<Interaction> OnInteract
        {
            add => _onInteract.AddListener(value);
            remove => _onInteract.RemoveListener(value);
        }
        
        
        public void Interact(Interaction interaction)
        {
            _onInteract?.Invoke(interaction);
        }

        public void InteractExit()
        {
            _onInteractExit?.Invoke();
        }

        public void InteractEnter()
        {
            _onInteractEnter?.Invoke();
        }
    }
}