using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reacative.Presentation.UI
{
    public class TextButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;
        
        public TextMeshProUGUI Text =>  _text;
        public Button Button =>  _button;
    }
}