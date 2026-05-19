using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Reacative.Domain.State;
using Reacative.Infrastructure.UI.Recruiting;
using Reacative.Presentation.UI.WindowsSystem;
using UnityEngine;

namespace Reacative.Presentation.UI
{
    public class RecruitingWindow : VirtualWindow, IRecruitingView
    {
        [Header("Recruiting Properties")]
        [SerializeField] private RecruitingItem _recruitingTemplate;
        [SerializeField] private Transform _recruitingContainer;
        
        private readonly Dictionary<string, RecruitingItem> _recruitingItems = new();
        private readonly List<RecruitingItem> _freeRecruitingItems = new();

        public async UniTask UpdateRecruitingItems(CatState[] catState, int cost)
        {
            foreach (var item in 
                     _recruitingItems.Keys.Where(item => catState.All(c => c.Id != item)))
            {
                _freeRecruitingItems.Add(_recruitingItems[item]);
                _recruitingItems.Remove(item);
                
            }

            foreach (var state in catState)
            {
                if (_recruitingItems.TryGetValue(state.Id, out var recruitingItem))
                {
                    await recruitingItem.UpdateCost(cost);
                    continue;
                }

                var item = _freeRecruitingItems.FirstOrDefault() ?? Instantiate(_recruitingTemplate, transform);
                _freeRecruitingItems.Remove(item);
                item.SetCatState(state);
                await item.UpdateCost(cost);
                _recruitingItems.Add(state.Id, item);
            }

            foreach (var item in _freeRecruitingItems)
            {
                Destroy(item);
            }
            _freeRecruitingItems.Clear();
        }

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