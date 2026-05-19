using System.Linq;
using Reacative.Domain;
using Reacative.Domain.Cats;
using Reacative.Domain.State;

namespace Reacative.Infrastructure.UI.Recruiting
{
    public class RecruitingController : BaseController<IRecruitingView>
    {
        private readonly Game _game;
        
        public RecruitingController(Game game)
        {
            _game = game;
            _game.OnStateChanged += (oldState, newState) =>
            {
                if (oldState.GeneratedCats.Equals(newState.GeneratedCats))
                {
                    return;
                }

                if (oldState.HiredCats.Equals(newState.HiredCats))
                {
                    return;
                }

                Update(View, newState);
            };
        }
        protected override void OnAssign(IRecruitingView view)
        {
            Update(view, _game.CurrentState);
        }

        private static void Update(IRecruitingView view, GameState gameState)
        {
            var availableCats = gameState.GetAvailableCats();
            view?.UpdateRecruitingItems(availableCats, 100);
        }
    }
}