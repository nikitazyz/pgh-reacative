using Reacative.Infrastructure.UI.ResourceDisplay;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

namespace Reacative.Presentation.UI
{
    public class ResourceDisplay : MonoBehaviour, IResourceDisplayView
    {
        [SerializeField] private WalletView _energyView;
        public void UpdateResources(double energy, double temperature)
        {
            _energyView.UpdateText((int)energy);
        }

        public bool IsActive => gameObject.activeSelf;

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}