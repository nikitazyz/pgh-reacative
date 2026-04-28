using TMPro;
using UnityEngine;

namespace Reacative.Presentation.UI
{
    public class TestResourceDisplay : ResourceDisplay
    {
        [SerializeField] private TMP_Text _text;
        public override void UpdateResources(double energy, double temperature)
        {
            _text.text = $"Energy: {energy:F0}\nTemperature: {temperature:F0}";
        }
    }
}