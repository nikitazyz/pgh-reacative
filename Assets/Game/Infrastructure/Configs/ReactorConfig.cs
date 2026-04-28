using System;
using UnityEngine;
using BetterInspector;

namespace Reacative.Domain.Configs
{
    [CreateAssetMenu(fileName = "ReactorConfig", menuName = "Configs/Reactor Config")]
    public class ReactorConfig : ScriptableObject, IReactorConfigProvider
    {
        [field: SerializeField]
        public double MaxTemperature {get; private set;}

        [field: SerializeField, Label(nameof(OverheatThreshold) + " (%)")]
        public double OverheatThreshold {get; private set;}

        [field: SerializeField]
        public double OverheatMultiplier {get; private set;}

        [field: SerializeField]
        public double BaseProduction {get; private set;}

        [field: SerializeField]
        public double LevelProductionMultiplier {get; private set;}

        [field: SerializeField]
        public double BaseTemperatureIncrease {get; private set;}

        [field: SerializeField]
        public double LevelTemperatureMultiplier {get; private set;}
    }
}