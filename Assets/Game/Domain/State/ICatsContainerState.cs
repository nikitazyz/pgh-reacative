using System.Collections.Generic;
using System.Collections.Immutable;

namespace Reacative.Domain.State
{
    public interface ICatsContainerState : IBuildingState
    {
        ImmutableList<string> ActiveCats { get; }
    }
}