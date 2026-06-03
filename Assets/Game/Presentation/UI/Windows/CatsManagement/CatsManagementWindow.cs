using System;
using System.Collections.Generic;
using System.Linq;
using Reacative.Domain.State;
using Reacative.Infrastructure.Buildings;
using Reacative.Infrastructure.Cats;
using Reacative.Infrastructure.Services;
using Reacative.Infrastructure.UI.CatsManagement;
using Reacative.Presentation.UI.WindowsSystem;
using UnityEngine;

namespace Reacative.Presentation.UI.Windows.CatsManagement
{
    public class CatsManagementWindow : VirtualWindow, ICatsManagementView
    {
        public event Action<BuildingsSet.BuildingType, CatState> RemoveCat; 
        public event Action<BuildingsSet.BuildingType, CatState> AddCat;

        [SerializeField] private GameObject _lockPanel;
        [SerializeField] private CatsSelectionPanel _catsSelectionPanel;
        [SerializeField] private CatsPanel[] _panels;

        private readonly Dictionary<string, CatsPanel> _catsPanels = new();
        private List<CatState> _availableCats = new();

        protected override void Awake()
        {
            base.Awake();

            _lockPanel.SetActive(true);
            _catsSelectionPanel.OnSelect += (type, cat) => AddCat?.Invoke(type, cat);
            
        }

        public void Init()
        {
            foreach (var panel in _panels)
            {
                if (!_catsPanels.TryAdd(BuildingsSet.IdFromType(panel.BuildingType), panel))
                {
                    panel.gameObject.SetActive(false);
                    continue;
                }

                panel.RemoveCat += cat => RemoveCat?.Invoke(panel.BuildingType, cat);
                panel.AddCat += () => _catsSelectionPanel.OpenPanel(panel.BuildingType, _availableCats);
            }
        }

        protected override void OnClosed()
        {
            base.OnClosed();
            _catsSelectionPanel.ClosePanel();
        }

        public void UpdateAvailablePanels(string[] availableBuildings)
        {
            foreach (var panel in _catsPanels.Values)
            {
                string id = BuildingsSet.IdFromType(panel.BuildingType);
                panel.gameObject.SetActive(availableBuildings.Contains(id));
            }
        }

        public void UpdateAvailableCats(List<CatState> catStates)
        {
            _availableCats = catStates;
        }

        public void UpdateCatsPanel(string buildingType, List<CatState> catStates)
        {
            var panel = _catsPanels[buildingType];
            panel.UpdateButtons(catStates);
        }

        public void Unlock()
        {
            _lockPanel.SetActive(false);
        }

        public bool IsActive => WindowState is WindowState.Opened or WindowState.Opening;

        public void SetActive(bool active)
        {
            if (active)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }
}