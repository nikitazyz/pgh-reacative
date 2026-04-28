using Reacative.Domain.Configs;
using UnityEngine;

namespace Reacative.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/Game Config")]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        [SerializeField] private ReactorConfig _reactor;
        [SerializeField] private TurbineConfig _turbine;

        public IReactorConfigProvider ReactorConfig => _reactor;
        public ITurbineConfigProvider TurbineConfig => _turbine;
    }
}