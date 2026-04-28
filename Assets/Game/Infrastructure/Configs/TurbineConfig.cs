using Reacative.Domain.Configs;
using UnityEngine;

namespace Reacative.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "TurbineConfig", menuName = "Configs/TurbineConfig")]
    public class TurbineConfig : ScriptableObject, ITurbineConfigProvider
    {
        [field: SerializeField]
        public double TurbineTime { get; private set; }
        [field: SerializeField]
        public double ReloadTime { get; private set; }
    }
}