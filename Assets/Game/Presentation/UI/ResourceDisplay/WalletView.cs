using TMPro;
using UnityEngine;

namespace Reacative.Presentation.UI
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _num1;
        [SerializeField] private TextMeshProUGUI _num2;
        [SerializeField] private TextMeshProUGUI _num3;
        [SerializeField] private TextMeshProUGUI _num4;

        public void Start()
        {
            _num1.text = _num2.text = _num3.text = _num4.text = "0";
        }

        public void UpdateText(int value)
        {
            // 0999
            if (value < 1000)
            {
                string text = value.ToString("0000");
                _num1.text = text[0].ToString();
                _num2.text = text[1].ToString();
                _num3.text = text[2].ToString();
                _num4.text = text[3].ToString();
                return;
            }

            // 99.9K

            if (value < 100_000)
            {
                int k = value / 1000;
                int m = (value % 1000) / 100;
                
                string thousands = k.ToString("00");
                string decimalPart = m.ToString("0");

                _num1.text = thousands[0].ToString();
                _num2.text = thousands[1].ToString();
                _num3.text = $"<color=red>{decimalPart}</color>";
                _num4.text = "K";
                return;
            }
            
            //999К

            string th = $"{value / 1000:000}K";
            _num1.text = th[0].ToString();
            _num2.text = th[1].ToString();
            _num3.text = th[2].ToString();
            _num4.text = "K";
        }
    }
}