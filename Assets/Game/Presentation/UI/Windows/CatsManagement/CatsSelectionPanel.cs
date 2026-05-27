using System;
using System.Collections.Generic;
using Reacative.Domain.State;
using Reacative.Infrastructure.Buildings;
using UnityEngine;

namespace Reacative.Presentation.UI.Windows.CatsManagement
{
    public class CatsSelectionPanel : MonoBehaviour
    {
        public event Action<BuildingsSet.BuildingType, CatState> OnSelect;
        [SerializeField] private CatsPanelButton _template;
        [SerializeField] private Transform _container;

        private readonly List<CatsPanelButton> _catsButtons = new();
        private BuildingsSet.BuildingType _selectedBuildingType;

        private void Awake()
        {
            OnSelect += (_,_) => gameObject.SetActive(false);
            _template.gameObject.SetActive(false);
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
    }
}