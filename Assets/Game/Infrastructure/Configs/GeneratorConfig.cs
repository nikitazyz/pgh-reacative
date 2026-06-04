using BetterInspector;
using Reacative.Domain.Configs;
using UnityEngine;

namespace Reacative.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "GeneratorConfig", menuName = "Configs/Generator Config")]
    public class GeneratorConfig : ScriptableObject, IGeneratorConfigProvider
    {
        [field: SerializeField, Label(nameof(PowerIncreaseSpeed)+" (%/click)")]
        public float PowerIncreaseSpeed { get; private set; }
        [field: SerializeField, Label(nameof(PowerDecreaseSpeed)+" (%/s)")]
        public float PowerDecreaseSpeed { get; private set; }
    }
}