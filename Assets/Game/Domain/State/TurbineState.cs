namespace Reacative.Domain.State
{
    public record TurbineState(
        int Level,
        bool IsActive,
        long ActivationTime
    )
    {
        public override string ToString()
        {
            return $"Level: {Level}\n"+
            $"IsActive: {IsActive}\n" +
            $"ActiveTime: {ActivationTime}";
        }
    }
}