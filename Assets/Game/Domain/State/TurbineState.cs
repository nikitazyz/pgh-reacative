using System.Collections.Immutable;

namespace Reacative.Domain.State
{
    public record TurbineState(
        int Level,
        bool IsActive,
        long ActivationTime,
        bool IsBought,
        ImmutableList<string> ActiveCats
    ) : IPurchasableBuildingState, ICatsContainerState
    {
        public override string ToString()
        {
            return $"Level: {Level}\n"+
            $"IsActive: {IsActive}\n" +
            $"ActiveTime: {ActivationTime}";
        }

        public static readonly string ID = "turbine";
        public string Id => ID;
    }
}