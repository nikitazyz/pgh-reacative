using Reacative.Domain.State;

namespace Reacative.Domain.Definitions
{

    public interface IPurchasableBuildingDefinition
    {
        string BuildingId { get; }
        int Cost { get; }
        public GameState GetBoughtState(GameState gameState);
        bool IsPurchased(GameState state);
    }
}