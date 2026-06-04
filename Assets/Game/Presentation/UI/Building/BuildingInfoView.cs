using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Reacative.Infrastructure.UI.BuildingInfo;
using Reacative.Presentation.UI.WindowsSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Reacative.Presentation.UI.Building
{
    public class BuildingInfoView : VirtualWindow, IBuildingInfoView
    {
        [SerializeField] private TextMeshProUGUI _nameField;
        [SerializeField] private TextMeshProUGUI _descriptionField;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _priceField;
        
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _cancelButton;

        [SerializeField] private LocalizedString _costString;
        
        public event Action OnBuyBuilding;
        public event Action OnCancelBuy;

        protected override void Awake()
        {
            base.Awake();
            _buyButton.onClick.AddListener(OnBuyClicked);
            _cancelButton.onClick.AddListener(OnCancelClicked);
        }

        private void OnBuyClicked()
        {
            OnBuyBuilding?.Invoke();
        }

        private void OnCancelClicked()
        {
            OnCancelBuy?.Invoke();
        }

        public async UniTask RequestBuilding(string name, string description, Sprite sprite, int cost, bool canBuy)
        {
            _nameField.text = name;
            _descriptionField.text = description;
            _icon.sprite = sprite;
            _priceField.text = await _costString.GetLocalizedStringAsync(cost);
            _buyButton.interactable = canBuy;
        }

        public bool IsActive => WindowState is WindowState.Opened or WindowState.Opening;

        public void SetActive(bool active)
        {
            if (active)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }
}