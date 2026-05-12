using System;
using System.Collections.Generic;
using System.Linq;
using Reacative.Presentation.InteractionSystem;
using UnityEngine;
using UnityEngine.EventSystems;
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
        
        private RectTransform _rectTransform;

        private void Awake()
        {
            _interactor.InteractEnter += (_) => SetCursor(_hover);
            _interactor.InteractExit += (_) => SetCursor(_normal);
            
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
            UnityEngine.Cursor.visible = false;
            
            _rectTransform = _image.GetComponent<RectTransform>();
        }

        private void Update()
        {
            transform.position = Mouse.current.position.ReadValue();
            
            CheckButtonOverSelectable();
        }

        private void CheckButtonOverSelectable()
        {
            PointerEventData pointerData =
                new PointerEventData(EventSystem.current);

            pointerData.position = Mouse.current.position.ReadValue();

            List<RaycastResult> results = new List<RaycastResult>();

            EventSystem.current.RaycastAll(pointerData, results);

            var firstHit = results.FirstOrDefault();
            var hitObject = firstHit.gameObject;
            if (hitObject == null)
                return;

            if (hitObject.GetComponentInParent<Selectable>())
            {
                Debug.Log(hitObject.name);
                SetCursor(_hover);
                return;
            }
            
            SetCursor(_normal);
        }

        private void SetCursor(Sprite sprite)
        {
            _image.sprite = sprite;
            _rectTransform.pivot = GetNormalizedPivot(sprite);
            _image.SetNativeSize();
        }

        private Vector2 GetNormalizedPivot(Sprite sprite)
        {
            Rect rect = sprite.rect;
            Vector2 pivot = sprite.pivot;
            
            pivot.x /= rect.width;
            pivot.y /= rect.height;
            return pivot;
        }
    }
}
