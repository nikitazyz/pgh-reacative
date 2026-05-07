using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Reacative.Presentation.UI
{
    public class Slider : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        public event Action<float> OnValueChanged; 
        
        [SerializeField] private RectTransform _sliderArea;
        [SerializeField] private RectTransform _handle;

        [SerializeField] private float _value;
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdateHandle();
                OnValueChanged?.Invoke(_value);
            }
        }

        public void SetValueWithoutNotify(float value)
        {
            _value = value;
            UpdateHandle();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            UpdateSlider(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            UpdateSlider(eventData);
        }

        private void UpdateSlider(PointerEventData eventData)
        {
            Vector2 localPoint;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _sliderArea,
                    eventData.position,
                    eventData.pressEventCamera,
                    out localPoint))
            {
                float width = _sliderArea.rect.width;

                float normalized = Mathf.InverseLerp(
                    -width * 0.5f,
                    width * 0.5f,
                    localPoint.x
                );

                normalized = Mathf.Clamp01(normalized);

                _value = Mathf.Lerp(_minValue, _maxValue, normalized);

                UpdateHandle();
                OnValueChanged?.Invoke(_value);
            }
        }

        private void UpdateHandle()
        {
            float width = _sliderArea.rect.width;
            if (_handle != null)
            {
                Vector2 handlePos = _handle.anchoredPosition;
                handlePos.x = Mathf.Lerp(
                    -width * 0.5f,
                    width * 0.5f,
                    _value
                );

                _handle.anchoredPosition = handlePos;
            }
        }
    }
}
