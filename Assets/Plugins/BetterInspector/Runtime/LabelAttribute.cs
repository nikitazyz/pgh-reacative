using UnityEngine;

namespace BetterInspector
{
    public class LabelAttribute : PropertyAttribute
    {
        public string Label;

        public LabelAttribute(string label)
        {
            Label = label;
        }
    }
}