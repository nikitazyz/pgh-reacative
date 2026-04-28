using System;
using Reacative.Presentation.InteractionSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Reacative.Presentation.UI.Cursor
{
    public class CursorUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _normal;
        [SerializeField] private Sprite _hover;

        [SerializeField] private Interactor _interactor;

        private void Awake()
        {
            _interactor.InteractEnter += (_) => _image.sprite = _hover;
            _interactor.InteractExit += (_) => _image.sprite = _normal;
            
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
            UnityEngine.Cursor.visible = false;
        }

        private void Update()
        {
            transform.position = Mouse.current.position.ReadValue();
        }
    }
}
