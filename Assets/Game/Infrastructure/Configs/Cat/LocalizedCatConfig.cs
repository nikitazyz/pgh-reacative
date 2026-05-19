using System.Collections.Generic;
using System.Linq;
using Reacative.Domain.Configs;
using Reacative.Infrastructure.Localization;
using UnityEngine;
using UnityEngine.Localization;

namespace Reacative.Infrastructure.Configs.Cat
{
    [CreateAssetMenu(menuName = "Configs/Cats Config", fileName = "CatsConfig", order = 0)]
    public class LocalizedCatConfig : ScriptableObject, ICatsConfigProvider
    {
        [field: SerializeField] public int HeadHunterSize { get; private set; } = 3;
        [field: SerializeField] public int HireBaseCost { get; private set; } = 10;
        [field: SerializeField] public int HireCostMultiplier { get; private set; } = 2;
        
        [SerializeField] private List<LocalizedString> _names;
        public IEnumerable<string> Names => _names.Select(n => n.ToLocalizationKey());
    }
}