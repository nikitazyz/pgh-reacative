using System;
using System.Collections.Generic;
using Reacative.Domain.State;
using Reacative.Infrastructure.Buildings;
using Reacative.Infrastructure.CameraSetup;
using Reacative.Infrastructure.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Reacative.Presentation.UI.Windows.CatsManagement
{
    public class CatsSelectionPanel : MonoBehaviour
    {
        public event Action<BuildingsSet.BuildingType, CatState> OnSelect;
        [SerializeField] private CatsPanelButton _template;
        [SerializeField] private RectTransform _container;

        private readonly List<CatsPanelButton> _catsButtons = new();
        private BuildingsSet.BuildingType _selectedBuildingType;

        private ICameraService _cameraService;

        private void Awake()
        {
            OnSelect += (_,_) => ClosePanel();
            _template.gameObject.SetActive(false);
        }

        private void Start()
        {
            _cameraService = ServiceLocator.GetService<ICameraService>();
        }

        public void OpenPanel(BuildingsSet.BuildingType type, List<CatState> states)
        {
            _selectedBuildingType = type;
            if (_catsButtons.Count < states.Count)
            {
                int makeCount = states.Count - _catsButtons.Count;
                for (int i = 0; i < makeCount; i++)
                {
                    var button = Instantiate(_template, _container);
                    button.gameObject.SetActive(true);
                    _catsButtons.Add(button);
                    button.OnClick += c => OnSelect?.Invoke(_selectedBuildingType, c);
                }
            }
            
            for (int i = 0; i < _catsButtons.Count; i++)
            {
                if (i >= states.Count)
                {
                    _catsButtons[i].gameObject.SetActive(false);
                    continue;
                }
                _catsButtons[i].CatState = states[i];
                _catsButtons[i].gameObject.SetActive(true);
            }
            
            gameObject.SetActive(true);
        }

        public void ClosePanel()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            var mouse = Mouse.current;
            var wasPressed = mouse.leftButton.wasPressedThisFrame;
            var position = mouse.position.ReadValue();
            var isInRect = RectTransformUtility.RectangleContainsScreenPoint(_container, position, _cameraService.UICamera);
            
            Debug.Log(isInRect);
            if (wasPressed && !isInRect)
            {
                ClosePanel();
            }
        }
    }
}