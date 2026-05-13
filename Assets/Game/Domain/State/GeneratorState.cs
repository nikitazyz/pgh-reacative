namespace Reacative.Domain.State
{
    public record GeneratorState(double Power) : IBuildingState
    {
        public static readonly string ID = "generator";
        public string Id => ID;
    }
}