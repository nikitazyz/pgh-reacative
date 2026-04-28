namespace Reacative.Domain.Configs
{
    public interface IConfigProvider
    {
        public IReactorConfigProvider ReactorConfig { get; }
        public ITurbineConfigProvider TurbineConfig { get; }
    }
}