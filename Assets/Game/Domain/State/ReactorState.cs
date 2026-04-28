using System.Collections.Generic;
using System.Collections.Immutable;

namespace Reacative.Domain.State
{
    public record ReactorState(
        int Level,
        double Temperature,
        bool IsActive,
        ImmutableList<string> ActiveCats
    ) : ICatsContainerState
    {
        IReadOnlyCollection<string> ICatsContainerState.ActiveCats => ActiveCats;

        public override string ToString()
        {
            return $"Level: {Level}\nTemp: {Temperature}\nIs Active: {IsActive}";
        }
    }
}