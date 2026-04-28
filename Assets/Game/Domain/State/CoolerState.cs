using System.Collections.Generic;
using System.Collections.Immutable;

namespace Reacative.Domain.State
{
    public record CoolerState(
        int Level,
        ImmutableList<string> ActiveCats
    ) : ICatsContainerState
    {
        IReadOnlyCollection<string> ICatsContainerState.ActiveCats => ActiveCats;

        public override string ToString()
        {
            return $"Level: {Level}";
        }
    }
}