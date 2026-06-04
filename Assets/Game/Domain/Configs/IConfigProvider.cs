namespace Reacative.Domain.Configs
{
    public interface IConfigProvider
    {
        public IReactorConfigProvider ReactorConfig { get; }
        public ITurbineConfigProvider TurbineConfig { get; }
        public IGeneratorConfigProvider GeneratorConfig { get; }
        public ICoolingConfigProvider CoolerConfig { get; }
        public ISpecialistConfigProvider SpecialistConfig { get; }
    }
}