using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Reacative.Domain;
using Reacative.Domain.Cats;
using Reacative.Domain.CommandSystem;
using Reacative.Domain.Configs;
using Reacative.Domain.Definitions.CatContainers;
using Reacative.Domain.State;
using Reacative.Infrastructure.Buildings;
using Reacative.Infrastructure.Services;
using UnityEngine;

namespace Reacative.Infrastructure.Cats
{
    public class CatsManager : IService
    {
        public event Action OnCatHired;
        public event Action<CatState, BuildingsSet.BuildingType> OnCatAdded;
        public event Action<CatState, BuildingsSet.BuildingType> OnCatRemoved;
        
        private readonly CatsGenerator _catsGenerator;
        private readonly GameSession _gameSession;
        private Dictionary<string, ICatsContainerDefinition> _definitions = new();

        private int HeadHunterSize { get; }

        private Game Game => _gameSession.CurrentGame;
        private GameState GameState => Game.CurrentState;
        
        private ICatsConfigProvider CatsConfigProvider { get; }

        public CatsManager(GameSession session, ICatsConfigProvider configProvider)
        {
            var nameGenerator = new CatNameGenerator(configProvider);
            var colorGenerator = new CatColorGenerator();
            _catsGenerator = new CatsGenerator(nameGenerator, colorGenerator);
            _gameSession = session;
            CatsConfigProvider = configProvider;
            HeadHunterSize = configProvider.HeadHunterSize;
        }

        public void AddDefinition(string id, ICatsContainerDefinition definition)
        {
            _definitions.Add(id, definition);
        }

        public async UniTask RefillHeadHunter()
        {
            var generatedCatsCount = GameState.GeneratedCats.Count;
            var hiredCatsCount = GameState.HiredCats.Count;

            var createCount = HeadHunterSize - (generatedCatsCount - hiredCatsCount);
            if (createCount <= 0)
            {
                return;
            }
            
            var cats = await _catsGenerator.GetNext(GameState, createCount);
            var addCatsCommand = new AddGeneratedCatsCommand(cats);
            
            Game.ExecuteCommand(addCatsCommand);
        }

        public void HireCat(CatState cat)
        {
            var hireCommand = new HireCatCommand(cat);
            var takeResource = new TakeResourceCommand(GetHireCost());
            if (!hireCommand.IsValid(Game))
            {
                throw new ArgumentException("Cat is already hired");
            }

            if (!takeResource.IsValid(Game))
            {
                return;
            }
            
            Game.ExecuteCommand(hireCommand);
            Game.ExecuteCommand(takeResource);
            OnCatHired?.Invoke();
        }

        public int GetHireCost()
        {
            return CatsConfigProvider.HireBaseCost + CatsConfigProvider.HireBaseCost * CatsConfigProvider.HireCostMultiplier *
                _gameSession.CurrentGame.CurrentState.GeneratedCats.Count;
        }

        public void SetCatOnBuilding(CatState cat, BuildingsSet.BuildingType building)
        {
            string id = BuildingsSet.IdFromType(building);
            var definition = _definitions[id];

            var command = new SetCatOnBuildingCommand(definition, cat.Id);
            if (!command.IsValid(Game))
            {
                return;
            }
            Game.ExecuteCommand(command);
            OnCatAdded?.Invoke(cat, building);
        }

        public void RemoveCatFromBuilding(CatState cat, BuildingsSet.BuildingType building)
        {
            string id = BuildingsSet.IdFromType(building);
            var definition = _definitions[id];

            var command = new RemoveCatFromBuildingCommand(definition, cat.Id);
            if (!command.IsValid(Game))
            {
                return;
            }
            Game.ExecuteCommand(command);
            OnCatRemoved?.Invoke(cat, building);
        }

        public IEnumerable<KeyValuePair<string, ICatsContainerDefinition>> GetAllDefinitions()
        {
            return _definitions;
        }

        public IEnumerable<string> GetActiveCats(BuildingsSet.BuildingType buildingType)
        {
            string id = BuildingsSet.IdFromType(buildingType);
            return _definitions[id].GetActiveCats(GameState);
        }
    }
}