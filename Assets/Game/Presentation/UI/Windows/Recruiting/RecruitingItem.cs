using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Reacative.Domain.State;
using Reacative.Presentation.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
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

        [SerializeField] private CatsResourcePack _catsResourcePack;

        private CatState _catState;
        
        private LocalizedString _localizedString;

        private void Awake()
        {
            
            _costString.StringChanged += value => _cost.text = value;
            _hireButton.onClick.AddListener(() => OnHire?.Invoke(_catState));
        }

        private void LocalizedStringOnStringChanged(string value)
        {
            _label.text = value;
        }

        public void SetCatState(CatState catState)
        {
            if (_localizedString == null)
            {
                _localizedString = new LocalizedString();
                _localizedString.TableReference = "names";
                _localizedString.StringChanged += LocalizedStringOnStringChanged;
            }
            _catState = catState;
            _label.text = _catState.Name;
            _localizedString.TableEntryReference = _catState.Name;
            var icon = _catsResourcePack.GetCatIcon(catState.Color);
            _icon.sprite = icon;
        }

        public async UniTask UpdateCost(int cost, bool canHire)
        {
            _costString.Arguments = new object[]
            {
                cost
            };

            _hireButton.interactable = canHire;
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
