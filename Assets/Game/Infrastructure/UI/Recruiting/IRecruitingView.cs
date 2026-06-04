using System;
using Cysharp.Threading.Tasks;
using Reacative.Domain.State;

namespace Reacative.Infrastructure.UI.Recruiting
{
    public interface IRecruitingView : IUIView
    {
        public event Action<CatState> OnHire; 
        public UniTask UpdateRecruitingItems(CatState[] catState, int cost, bool canHire);
        public void Unlock();
    }
}