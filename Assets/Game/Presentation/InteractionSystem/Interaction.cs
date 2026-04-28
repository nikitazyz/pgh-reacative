using UnityEngine;

namespace Reacative.Presentation.InteractionSystem
{
    public record Interaction(bool LeftMouseButton, bool RightMouseButton, Vector3 Position, Vector3 HitPosition);
}