using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Reacative.Domain.State;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Reacative.Presentation.UI
{
    public class RecruitingItem : MonoBehaviour
    {
        public event Action<CatState> OnHire;
        
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private TextMeshProUGUI _cost;
        [SerializeField] private Button _hireButton;
        [SerializeField] private Image _icon;
        [SerializeField] private LocalizedString _costString;

        private CatState _catState;

        private void Awake()
        {
            _costString.StringChanged += value => _cost.text = value;
            _hireButton.onClick.AddListener(() => OnHire?.Invoke(_catState));
        }

        public void SetCatState(CatState catState)
        {
            _catState = catState;
            _label.text = _catState.Name;
        }

        public async UniTask UpdateCost(int cost)
        {
            _costString.Arguments = new object[]
            {
                cost
            };

            await SetCostUI(cost);
        }

        private async UniTask SetCostUI(int cost)
        {
            _cost.text = cost.ToString();
            var localizedString = await _costString.GetLocalizedStringAsync();
            _cost.text = localizedString;
        }
    }
}
