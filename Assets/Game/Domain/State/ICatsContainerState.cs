using System.Collections.Generic;

namespace Reacative.Domain.State
{
    public interface ICatsContainerState
    {
        IReadOnlyCollection<string> ActiveCats { get; }
    }
}