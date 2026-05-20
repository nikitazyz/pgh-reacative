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

        public IReactorConfigProvider ReactorConfig => _reactor;
        public ITurbineConfigProvider TurbineConfig => _turbine;
        public IGeneratorConfigProvider GeneratorConfig => _generatorConfig;
        public LocalizedCatConfig CatsConfig => _catsConfig;
    }
}