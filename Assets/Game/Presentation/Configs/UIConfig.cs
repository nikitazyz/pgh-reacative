using Reacative.Infrastructure.Services;
using Reacative.Presentation.UI;
using UnityEngine;

namespace Reacative.Presentation.Configs
{
    [CreateAssetMenu(fileName = "UIConfig", menuName = "Configs/UI Config", order = 0)]
    public class UIConfig : ScriptableObject, IService
    {
        [SerializeField] private ResourceDisplay _resourceDisplay;
        public ResourceDisplay ResourceDisplay => _resourceDisplay;
    }
}