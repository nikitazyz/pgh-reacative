using UnityEngine;
using UnityEngine.InputSystem;

namespace Reacative.Presentation.CameraControl
{
    public class CameraInput : MonoBehaviour
    {
        [SerializeField] private InputActionReference _moveInput;
        [SerializeField] private CameraFieldControl _fieldControl;

        private Vector2 _moveDirection;
    
        private void Awake()
        {
            _moveInput.action.performed += MoveActionOnPerformed;
            _moveInput.action.canceled += MoveActionOnPerformed;
        }

        private void OnEnable()
        {
            _moveInput.action.Enable();
        }

        private void OnDisable()
        {
            _moveDirection = Vector2.zero;
            _moveInput.action.Disable();
        }

        private void OnDestroy()
        {
            _moveInput.action.performed -= MoveActionOnPerformed;
            _moveInput.action.canceled -= MoveActionOnPerformed;
        }

        private void MoveActionOnPerformed(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();
            _moveDirection = input;
        }

        private void Update()
        {
            _fieldControl.Move(_moveDirection);
        }
    }
}
