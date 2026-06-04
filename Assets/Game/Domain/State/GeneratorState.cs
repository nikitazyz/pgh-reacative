using System.Collections.Immutable;

namespace Reacative.Domain.State
{
    public record GeneratorState(double Power, ImmutableList<string> ActiveCats) : IBuildingState, ICatsContainerState
    {
        public static readonly string ID = "generator";
        public string Id => ID;
    }
}