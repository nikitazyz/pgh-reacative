using Reacative.Domain.State;
using Reacative.Domain.Calculators;
using Reacative.Domain.Configs;

namespace Reacative.Domain.Simulation
{
    public class TurbineSimulation : ISimulationSystem
    {
        private readonly ITurbineConfigProvider _config;

        public TurbineSimulation(ITurbineConfigProvider config)
        {
            _config = config;
        }
        
        public void Simulate(GameState gameState, SimulationContext context)
        {
            if (!gameState.TurbineState.IsActive) return;

            var turbine = gameState.TurbineState;
            long turbineShutDown = turbine.ActivationTime + (long)(_config.TurbineTime * 1000);
            var energyProduced = context.ProducedEnergy;
            if (turbineShutDown < context.TimeStamp)
            {
                var turbineTime = (turbineShutDown - gameState.LastUpdateTime) / 1000d;
                var k = turbineTime / context.DeltaTime;
                
                var bonusEnergy = energyProduced * k;
                
                var energyBon = TurbineCalculator.CalculateEnergyBonus(bonusEnergy, gameState.TurbineState.Level);
                context.ProducedEnergy += energyBon;
                context.ShouldStopTurbine = true;
                return;
            }

            var energyBonus = TurbineCalculator.CalculateEnergyBonus(context.ProducedEnergy, gameState.TurbineState.Level);
            context.ProducedEnergy += energyBonus;
        }
    }
}