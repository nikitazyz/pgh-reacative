using Reacative.Domain.State;

namespace Reacative.Domain.Definitions
{

    public interface IPurchasableBuildingDefinition
    {
        int Cost { get; }

        public GameState GetBoughtState(GameState gameState);
        bool IsPurchased(GameState state);
    }
}