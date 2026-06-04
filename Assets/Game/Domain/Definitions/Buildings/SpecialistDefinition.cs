using Reacative.Domain.State;

namespace Reacative.Domain.Definitions
{
    public class SpecialistDefinition : PurchasableBuildingDefinition
    {
        public override string BuildingId => SpecialistState.ID;
        public override int Cost { get; }

        public SpecialistDefinition(int cost)
        {
            Cost = cost;
        }

        public override GameState GetBoughtState(GameState gameState)
        {
            return gameState with
            {
                SpecialistState = gameState.SpecialistState with
                {
                    IsBought = true
                }
            };
        }

        public override bool IsPurchased(GameState state)
        {
            return state.SpecialistState.IsBought;
        }
    }
}