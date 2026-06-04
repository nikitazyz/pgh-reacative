using UnityEngine;

namespace Reacative.Infrastructure.InteractionSystem
{
    public record Interaction(bool LeftMouseButton, bool RightMouseButton, Vector3 Position, Vector3 HitPosition);
}