using Reacative.Domain.State;

namespace Reacative.Domain.Definitions
{
    public class TurbineDefinition : PurchasableBuildingDefinition
    {
        public override int Cost { get; }

        public TurbineDefinition(int cost)
        {
            Cost = cost;
        }
        public override GameState GetBoughtState(GameState gameState)
        {
            return gameState with { TurbineState = gameState.TurbineState with{ IsBought = true } };
        }

        public override bool IsPurchased(GameState state)
        {
            return state.TurbineState is { IsBought: true };
        }
    }
}