using Reacative.Domain.Resources;

namespace Reacative.Domain.State
{
    public record ResourceState(
        ResourceType Type,
        double Amount
    );
}