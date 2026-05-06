using System;
using System.Collections.Generic;
using Reacative.Domain.CommandSystem;
using Reacative.Domain.Definitions;
using Reacative.Infrastructure.Services;
using UnityEngine;

namespace Reacative.Infrastructure.Buildings
{
    public class BuildingShop : IService
    {
        public event Action<IPurchasableBuildingDefinition> OnBuildingBought;
        
        private readonly Dictionary<string, IPurchasableBuildingDefinition> _definitions = new();
        private readonly GameSession _gameSession;

        public BuildingShop(GameSession gameSession)
        {
            _gameSession = gameSession;
        }

        public void RegisterDefinition(string id, IPurchasableBuildingDefinition definition)
        {
            _definitions.Add(id, definition);
        }

        public void BuyBuilding(BuildingsSet.BuildingType type)
        {
            BuyBuilding(BuildingsSet.IdFromType(type));
        }
        
        public void BuyBuilding(string id)
        {
            if (!_definitions.TryGetValue(id, out var definition))
            {
                return;
            }
            
            
            var buyBuilding = new BuyBuildingCommand(definition);
            if (!buyBuilding.IsValid(_gameSession.CurrentGame))
            {
                return;
            }
            _gameSession.CurrentGame.ExecuteCommand(buyBuilding);
            Debug.Log($"Buying {id}");
            OnBuildingBought?.Invoke(definition);
        }

        public bool IsBought(BuildingsSet.BuildingType type)
        {
            return IsBought(BuildingsSet.IdFromType(type));
        }
        
        public bool IsBought(string id)
        {
            return _definitions[id].IsPurchased(_gameSession.CurrentGame.CurrentState);
        }
    }
}