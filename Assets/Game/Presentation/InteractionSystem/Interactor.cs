using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Reacative.Presentation.InteractionSystem
{
    public class Interactor : MonoBehaviour
    {
        public event Action<IInteractable> InteractEnter;
        public event Action<IInteractable> InteractExit;

        public event Action<IInteractable, Interaction> Interacted;
        
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _interactionMask;

        private GameObject _lastHitObject;

        private void Update()
        {
            var mousePosition = Mouse.current.position.ReadValue();

            var isOverUI = EventSystem.current.IsPointerOverGameObject();
            if (isOverUI)
            {
                ExitLast(null);
                _lastHitObject = null;
                return;
            }

            if (!Physics.Raycast(_camera.ScreenPointToRay(mousePosition), out var hit, Mathf.Infinity, _interactionMask))
            {
                ExitLast(null);
                _lastHitObject = null;
                return;
            }

            ExitLast(hit.collider.gameObject);
            
            EnterNew(hit.collider.gameObject);
            _lastHitObject = hit.collider.gameObject;

            Interact(mousePosition, hit);
        }

        private void Interact(Vector2 mousePosition, RaycastHit hit)
        {
            if (!_lastHitObject)
            {
                return;
            }
            Interaction interaction = null;
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                interaction = new Interaction(true, false, mousePosition, hit.point);
            }
            else if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                interaction = new Interaction(false, true, mousePosition, hit.point);
            }

            
            if (interaction != null)
            {
                var interactable = _lastHitObject.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact(interaction);
                    Interacted?.Invoke(interactable, interaction);
                }
            }
        }

        private void EnterNew(GameObject current)
        {
            if (_lastHitObject != current && current)
            {
                var interactable = current.GetComponent<IInteractable>();
                if (interactable is IInteractableEnter enter)
                {
                    enter.InteractEnter();
                    InteractEnter?.Invoke(enter);
                }
            }
        }

        private void ExitLast(GameObject current)
        {
            if (_lastHitObject != current && _lastHitObject)
            {
                var interactable = _lastHitObject.GetComponent<IInteractable>();
                if (interactable is IInteractableExit exit)
                {
                    exit.InteractExit();
                    InteractExit?.Invoke(exit);
                }
                
            }
        }
    }
}
