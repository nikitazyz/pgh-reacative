using UnityEditor;
using UnityEngine;

namespace BetterInspector
{
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            LabelAttribute attr = (LabelAttribute)attribute;

            EditorGUI.PropertyField(position, property, new GUIContent(attr.Label));
        }
    }
}