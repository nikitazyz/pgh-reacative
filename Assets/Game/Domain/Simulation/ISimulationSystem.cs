using Reacative.Domain.State;

namespace Reacative.Domain.Simulation
{
    public interface ISimulationSystem
    {
        public void Simulate(GameState gameState, SimulationContext context);
    }
}