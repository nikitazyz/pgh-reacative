using System;
using Reacative.Domain.Configs;
using Reacative.Domain.State;

namespace Reacative.Domain.Simulation
{
    public class GeneratorSimulation : ISimulationSystem
    {
        private readonly IGeneratorConfigProvider _generatorConfigProvider;

        public GeneratorSimulation(IGeneratorConfigProvider generatorConfigProvider)
        {
            _generatorConfigProvider = generatorConfigProvider;
        }
        public void Simulate(GameState gameState, SimulationContext context)
        {
            var power = gameState.GeneratorState.Power;

            if (power > 0)
            {
                var deltaPower = _generatorConfigProvider.PowerDecreaseSpeed * 0.01 * context.DeltaTime;
                power = Math.Max(0, power - deltaPower);
            }

            context.GeneratorPower = power;
        }
    }
}