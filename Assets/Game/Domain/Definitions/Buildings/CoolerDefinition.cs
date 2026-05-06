using Reacative.Domain.State;

namespace Reacative.Domain.Definitions
{
    public class CoolerDefinition : PurchasableBuildingDefinition
    {
        public override int Cost { get; }

        public CoolerDefinition(int cost)
        {
            Cost = cost;
        }
        public override GameState GetBoughtState(GameState gameState)
        {
            return gameState with { CoolerState = gameState.CoolerState with { IsBought = true } };
        }

        public override bool IsPurchased(GameState state)
        {
            return state.CoolerState is { IsBought: true };
        }
    }
}