using System;
using Reacative.Domain.State;
using Reacative.Presentation.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Reacative.Presentation.UI.Windows.CatsManagement
{
    public class CatsPanelButton : MonoBehaviour
    {
        public event Action<CatState> OnClick;

        public CatState CatState
        {
            get => _catState;
            set
            {
                _catState = value;
                _image.sprite = _resourcePack.GetCatIcon(_catState.Color);
            }
        }

        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private CatsResourcePack _resourcePack;
        private CatState _catState;

        private void Awake()
        {
            _button.onClick.AddListener(() => OnClick?.Invoke(CatState));
        }
    }
}