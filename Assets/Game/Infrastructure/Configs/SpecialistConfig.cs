using Reacative.Domain.Configs;
using UnityEngine;

namespace Reacative.Infrastructure.Configs
{
    [CreateAssetMenu(menuName = "Configs/Specialist Config", fileName = "SpecialistConfig", order = 0)]
    public class SpecialistConfig : ScriptableObject, ISpecialistConfigProvider
    {
        [field: SerializeField]
        public int Cost { get; private set; }
    }
}