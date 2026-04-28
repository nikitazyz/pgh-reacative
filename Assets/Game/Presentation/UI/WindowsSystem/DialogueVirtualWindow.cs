using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Reacative.Presentation.UI.WindowsSystem
{
    public class DialogueVirtualWindow : VirtualWindow
    {
        [Header("Dialogue Settings")]
        [SerializeField] private TMP_Text _message;
        [SerializeField] private TextButton _buttonLeft;
        [SerializeField] private TextButton _buttonRight;
        
        public string Message { get => _message.text; set => _message.text = value; }
        public string LeftButtonText
        {
            get => _buttonLeft.Text.text;
            set => _buttonLeft.Text.text = value;
        }

        public string RightButtonText
        {
            get => _buttonRight.Text.text;
            set => _buttonRight.Text.text = value;
        }

        private UnityAction _leftButtonCallback;
        private UnityAction _rightButtonCallback;

        protected override void Awake()
        {
            base.Awake();
            _buttonLeft.Button.onClick.AddListener(OnLeftClick);
            _buttonRight.Button.onClick.AddListener(OnRightClick);
        }

        private void OnRightClick()
        {
            _rightButtonCallback?.Invoke();
            Close();
        }

        private void OnLeftClick()
        {
            _leftButtonCallback?.Invoke();
            Close();
        }

        public void Open(string message, UnityAction leftButtonCallback = null, UnityAction rightButtonCallback = null, 
            string leftButton = "Yes", string rightButton = "No")
        {
            _message.text = message;
            _buttonLeft.Text.text = leftButton;
            _buttonRight.Text.text = rightButton;
            _leftButtonCallback = leftButtonCallback;
            _rightButtonCallback = rightButtonCallback;
            base.Open();
        }
    }
}
