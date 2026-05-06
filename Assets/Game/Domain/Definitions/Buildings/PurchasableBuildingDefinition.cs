using Reacative.Domain.State;

namespace Reacative.Domain.Definitions
{
    public abstract class PurchasableBuildingDefinition : IPurchasableBuildingDefinition
    {
        public abstract int Cost { get; }
        public abstract GameState GetBoughtState(GameState gameState);
        public abstract bool IsPurchased(GameState state);
    }
}