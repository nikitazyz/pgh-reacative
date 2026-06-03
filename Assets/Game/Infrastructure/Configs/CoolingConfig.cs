using Reacative.Domain.Configs;
using UnityEngine;

namespace Reacative.Infrastructure.Configs
{
    [CreateAssetMenu(menuName = "Configs/Cooling Config", fileName = "CoolingConfig", order = 0)]
    public class CoolingConfig : ScriptableObject, ICoolingConfigProvider
    {
        [field: SerializeField]
        public int Cost { get; private set; }
    }
}