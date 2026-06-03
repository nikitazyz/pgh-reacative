namespace Reacative.Domain.State
{
    public record SpecialistState(bool IsBought) : IPurchasableBuildingState
    {
        public static string ID => "specialist";
        public string Id => ID;
    }
}