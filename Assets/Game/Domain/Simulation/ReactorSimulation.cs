using Reacative.Domain.Calculators;
using Reacative.Domain.Configs;
using Reacative.Domain.State;

namespace Reacative.Domain.Simulation
{
    public class ReactorSimulation : ISimulationSystem
    {

        private IReactorConfigProvider _config;

        public ReactorSimulation(IReactorConfigProvider config)
        {
            _config = config;
        }
 
        public void Simulate(GameState gameState, SimulationContext context)
        {
            var deltaTime = context.DeltaTime;
            if (!gameState.ReactorState.IsActive)
            {
                InactiveSimulation(gameState, context);
                return;
            }
            

            ActiveStateSimulation(gameState, context);
        }

        private void InactiveSimulation(GameState gameState, SimulationContext context)
        {
            var newTemperature = ReactorCalculator.CalculateTemperatureDecrease(gameState.ReactorState.Temperature, context.DeltaTime, _config.BaseTemperatureIncrease, _config.LevelTemperatureMultiplier, gameState.ReactorState.Level);

            var temperatureDelta = newTemperature - gameState.ReactorState.Temperature;
            context.TemperatureDelta = temperatureDelta;
        }

        private void ActiveStateSimulation(GameState gameState, SimulationContext context)
        {
            double overheatTemp = _config.MaxTemperature * _config.OverheatThreshold;
            bool isOverheated = gameState.ReactorState.Temperature >= overheatTemp;
            
            var newTemperature = ReactorCalculator.CalculateTemperatureIncrease(gameState.ReactorState.Temperature, 
                _config.BaseTemperatureIncrease, 
                context.DeltaTime, 
                _config.LevelTemperatureMultiplier, 
                gameState.ReactorState.Level, _config.MaxTemperature);
            
            bool willOverheat = newTemperature >= overheatTemp;

            if (willOverheat && !isOverheated)
            {
                var overheatTime = (overheatTemp - gameState.ReactorState.Temperature) / 
                (_config.BaseTemperatureIncrease + _config.LevelTemperatureMultiplier * gameState.ReactorState.Level);

                var productionUntilOverheat = ReactorCalculator.CalculateEnergyProduction(_config.BaseProduction, overheatTime, _config.LevelProductionMultiplier, gameState.ReactorState.Level);
                var productionAfterOverheat = ReactorCalculator.CalculateEnergyProduction(_config.BaseProduction, context.DeltaTime - overheatTime, _config.LevelProductionMultiplier, gameState.ReactorState.Level);
                var overheatProduction = ReactorCalculator.CalculateOverheatEnergyProduction(productionAfterOverheat, _config.OverheatMultiplier);
                var totalProduction = productionUntilOverheat + overheatProduction;

                context.TemperatureDelta = newTemperature - gameState.ReactorState.Temperature;
                context.ProducedEnergy = totalProduction;

                return;
            }

            var production = ReactorCalculator.CalculateEnergyProduction(_config.BaseProduction, context.DeltaTime, _config.LevelProductionMultiplier, gameState.ReactorState.Level);
            if (isOverheated)
            {
                production = ReactorCalculator.CalculateOverheatEnergyProduction(production, _config.OverheatMultiplier);
            }

            context.TemperatureDelta = newTemperature - gameState.ReactorState.Temperature;
            context.ProducedEnergy = production;
        }
    }
}