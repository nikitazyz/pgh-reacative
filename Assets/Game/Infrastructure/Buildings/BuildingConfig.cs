using UnityEngine;

namespace Reacative.Infrastructure.Buildings
{
    [CreateAssetMenu(menuName = "Buildings/BuildingConfig")]
    public class BuildingConfig : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _cost;
        
        public Sprite Icon => _icon;
        public string Name => _name;
        public string Description => _description;
        public int Cost => _cost;
    }
}