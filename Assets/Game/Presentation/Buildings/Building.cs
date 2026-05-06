using Reacative.Domain.Definitions;
using Reacative.Infrastructure.Buildings;
using Reacative.Infrastructure.Services;
using UnityEngine;

namespace Reacative.Presentation.Buildings
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private GameObject _building;
        [SerializeField] private BuildingField _buildingField;
        [SerializeField] private BuildingsSet.BuildingType _type;

        private BuildingShop _shop;
        private void Start()
        {
            _shop = ServiceLocator.GetService<BuildingShop>();
            if (_shop.IsBought(_type))
            {
                _building.SetActive(true);
                _buildingField.gameObject.SetActive(false);
            }
            else
            {
                _building.SetActive(false);
                _buildingField.gameObject.SetActive(true);
                _buildingField.OnPurchase += OnPurchase;
                _shop.OnBuildingBought += OnBought;
            }
        }

        private void OnBought(IPurchasableBuildingDefinition iPurchasableBuildingDefinition)
        {
            if (!_shop.IsBought(_type)) return;
            
            _building.SetActive(true);
            _buildingField.gameObject.SetActive(false);
            _buildingField.OnPurchase -= OnPurchase;
            _shop.OnBuildingBought -= OnBought;
        }

        private void OnPurchase()
        {
            _shop.BuyBuilding(_type);
        }
    }
}