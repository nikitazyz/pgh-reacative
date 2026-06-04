using System;
using System.Collections.Generic;
using Reacative.Domain.State;
using Reacative.Infrastructure.Buildings;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Reacative.Presentation.UI.Windows.CatsManagement
{
    public class CatsPanel : MonoBehaviour
    {
        public event Action<CatState> RemoveCat;
        public event Action AddCat;

        [SerializeField] private BuildingsSet.BuildingType _buildingType;
        [SerializeField] private CatsPanelButton[] _buttons;
        [SerializeField] private Button _addButton;

        private List<CatState> _catStates = new();

        public BuildingsSet.BuildingType BuildingType => _buildingType;

        private void Awake()
        {
            UpdateButtons(_catStates);

            foreach (var button in _buttons)
            {
                button.OnClick += RemoveCat;
            }
            
            _addButton.onClick.AddListener(() => AddCat?.Invoke());
        }

        public void UpdateButtons(List<CatState> catStates)
        {
            _catStates = catStates;
            for (int i = 0; i < _buttons.Length; i++)
            {
                if (i >= _catStates.Count)
                {
                    _buttons[i].gameObject.SetActive(false);
                    continue;
                }

                CatState catState = _catStates[i];
                _buttons[i].gameObject.SetActive(true);
                _buttons[i].CatState = catState;
            }

            _addButton.gameObject.SetActive(catStates.Count < _buttons.Length);
        }
    }
}