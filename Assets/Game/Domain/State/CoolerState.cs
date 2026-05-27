using System.Collections.Generic;
using System.Collections.Immutable;

namespace Reacative.Domain.State
{
    public record CoolerState(
        int Level,
        ImmutableList<string> ActiveCats,
        bool IsBought
    ) : ICatsContainerState, IPurchasableBuildingState
    {
        public override string ToString()
        {
            return $"Level: {Level}";
        }

        public static readonly string ID = "cooler";
        public string Id => ID;
    }
}