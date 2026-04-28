using Reacative.Infrastructure.UI.ResourceDisplay;
using UnityEngine;
using TMPro;

namespace Reacative.Presentation.UI
{
    public abstract class ResourceDisplay : MonoBehaviour, IResourceDisplayView
    {
        public abstract void UpdateResources(double energy, double temperature);

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}