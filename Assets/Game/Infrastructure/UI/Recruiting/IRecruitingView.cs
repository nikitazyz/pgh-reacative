using Cysharp.Threading.Tasks;
using Reacative.Domain.State;

namespace Reacative.Infrastructure.UI.Recruiting
{
    public interface IRecruitingView : IUIView
    {
        public UniTask UpdateRecruitingItems(CatState[] catState, int cost);
    }
}