using Reacative.Infrastructure.Buildings;
using UnityEngine;
using UnityEngine.Localization;

namespace Reacative.Infrastructure.Configs.BuildingsMeta
{
    [CreateAssetMenu(menuName = "Configs/MetaData/Building", fileName = "BuildingMetaData", order = 0)]
    public class BuildingMetaData : ScriptableObject
    {
        [SerializeField] private BuildingsSet.BuildingType _type;
        [SerializeField] private LocalizedString _name;
        [SerializeField] private LocalizedString _description;
        [SerializeField] private Sprite _icon;

        public BuildingsSet.BuildingType Type => _type;
        public LocalizedString Name => _name;
        public LocalizedString Description => _description;
        public Sprite Icon => _icon;
    }
}