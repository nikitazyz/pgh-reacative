using Reacative.Domain.State;

namespace Reacative.Domain.Definitions
{
    public class LabDefinition : PurchasableBuildingDefinition
    {
        public override int Cost { get; }
        
        public override GameState GetBoughtState(GameState gameState)
        {
            return gameState with
            {
                LabState = gameState.LabState with
                {
                    IsBought = true
                }
            };
        }

        public override bool IsPurchased(GameState state)
        {
            return state.LabState.IsBought;
        }

        public LabDefinition(int cost)
        {
            Cost = cost;
        }
    }
}