namespace Reacative.Domain.Configs
{
    public interface ITurbineConfigProvider
    {
        public double TurbineTime { get; }
        public double ReloadTime { get; }
    }
}