namespace Reacative.Domain.Configs
{
    public interface IReactorConfigProvider {
        public double MaxTemperature { get; }
        public double OverheatThreshold { get; }
        public double OverheatMultiplier { get; }
        public double BaseProduction { get; }
        public double LevelProductionMultiplier { get; }
        public double BaseTemperatureIncrease { get; }
        public double LevelTemperatureMultiplier { get; }
    }
}