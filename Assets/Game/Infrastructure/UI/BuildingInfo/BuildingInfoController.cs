using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Reacative.Infrastructure.Buildings;
using Reacative.Infrastructure.Configs.BuildingsMeta;
using Reacative.Infrastructure.Services;
using UnityEngine;

namespace Reacative.Infrastructure.UI.BuildingInfo
{
    public class BuildingInfoController : BaseController<IBuildingInfoView>
    {
        private readonly BuildingShop _buildingShop;
        private BuildingsSet.BuildingType _currentBuilding;
        private readonly Dictionary<BuildingsSet.BuildingType, BuildingMetaData> _buildingMetaData;

        public BuildingInfoController(IEnumerable<BuildingMetaData> metaData)
        {
            _buildingShop = ServiceLocator.GetService<BuildingShop>();
            _buildingShop.OnBuildingRequest += type => SetBuildingData(type).Forget();
            _buildingMetaData = metaData.ToDictionary(m => m.Type);
        }

        protected override void OnAssign(IBuildingInfoView view)
        {
            view.OnBuyBuilding += OnBuyBuilding;
            view.OnCancelBuy += OnCancelBuilding;
        }

        private void OnCancelBuilding()
        {
            SetActive(false);
        }

        private void OnBuyBuilding()
        {
            _buildingShop.BuyBuilding(_currentBuilding);
            SetActive(false);
        }

        private async UniTaskVoid SetBuildingData(BuildingsSet.BuildingType type)
        {
            try
            {
                _currentBuilding = type;
                var nameTask = _buildingMetaData[type].Name.GetLocalizedStringAsync().Task.AsUniTask();
                var descriptionTask = _buildingMetaData[type].Description.GetLocalizedStringAsync().Task.AsUniTask();
                var (name, description) = await UniTask.WhenAll(nameTask, descriptionTask);
                Sprite icon = _buildingMetaData[type].Icon;
                int cost = _buildingShop.GetCost(type);
                bool canBuy = _buildingShop.CanBuy(type);
                await View.RequestBuilding(name, description, icon, cost, canBuy);
                SetActive(true);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}