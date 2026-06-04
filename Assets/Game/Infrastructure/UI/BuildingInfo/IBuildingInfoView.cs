using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Reacative.Infrastructure.UI.BuildingInfo
{
    public interface IBuildingInfoView : IUIView
    {
        public event Action OnBuyBuilding;
        public event Action OnCancelBuy;
        public UniTask RequestBuilding(string name, string description, Sprite sprite, int cost, bool canBuy);
    }
}