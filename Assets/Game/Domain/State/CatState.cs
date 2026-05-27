using Reacative.Domain.Cats;

namespace Reacative.Domain.State
{
    public record CatState(
        string Id,
        string Name,
        CatColor Color,
        string BuildingId
    );
}