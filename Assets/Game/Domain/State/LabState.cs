namespace Reacative.Domain.State
{
    public record LabState(bool IsBought) : IPurchasableBuildingState
    {
        public static readonly string ID = "lab";
        public string Id => ID;
    }
}