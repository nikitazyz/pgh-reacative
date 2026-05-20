using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Reacative.Domain.Cats;
using Reacative.Domain.Configs;
using Reacative.Infrastructure.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace Reacative.Infrastructure.Configs.Cat
{
    [CreateAssetMenu(menuName = "Configs/Cats Config", fileName = "CatsConfig", order = 0)]
    public class LocalizedCatConfig : ScriptableObject, ICatsConfigProvider
    {
        [field: SerializeField] public int HeadHunterSize { get; private set; } = 3;
        [field: SerializeField] public int HireBaseCost { get; private set; } = 10;
        [field: SerializeField] public int HireCostMultiplier { get; private set; } = 2;

        [SerializeField] private TableReference _tableReference;
        
        
        public async Task<string[]> GetNames()
        {
            StringTable table = await LocalizationSettings.StringDatabase.GetTableAsync(_tableReference).Task;
            return table.Values.Select(x => x.Key).ToArray();
        }
        
        private string[] _namesList;
    }
}