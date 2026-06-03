using System;
using System.Collections.Generic;
using Reacative.Domain.State;
using Reacative.Infrastructure.Buildings;

namespace Reacative.Infrastructure.UI.CatsManagement
{
    public interface ICatsManagementView : IUIView
    {
        public event Action<BuildingsSet.BuildingType, CatState> RemoveCat; 
        public event Action<BuildingsSet.BuildingType, CatState> AddCat;
        public void UpdateCatsPanel(string buildingType, List<CatState> catStates);
        public void UpdateAvailablePanels(string[] availableBuildings);
        public void Unlock();
        void UpdateAvailableCats(List<CatState> catStates);
        void Init();
    }
}