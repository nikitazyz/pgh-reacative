using Reacative.Domain.Configs;
using Reacative.Infrastructure.Configs.Cat;
using UnityEngine;

namespace Reacative.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/Game Config")]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        [SerializeField] private ReactorConfig _reactor;
        [SerializeField] private TurbineConfig _turbine;
        [SerializeField] private GeneratorConfig _generatorConfig;
        [SerializeField] private LocalizedCatConfig _catsConfig;
        [SerializeField] private CoolingConfig _coolingConfig;
        [SerializeField] private SpecialistConfig _specialistConfig;

        public IReactorConfigProvider ReactorConfig => _reactor;
        public ITurbineConfigProvider TurbineConfig => _turbine;
        public IGeneratorConfigProvider GeneratorConfig => _generatorConfig;
        public ICoolingConfigProvider CoolerConfig => _coolingConfig;
        public LocalizedCatConfig CatsConfig => _catsConfig;
        public ISpecialistConfigProvider SpecialistConfig => _specialistConfig;
    }
}