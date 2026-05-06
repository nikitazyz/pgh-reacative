namespace Reacative.Domain.State
{
    public interface IPurchasableBuildingState : IBuildingState
    {
        public bool IsBought { get; }
    }
}