using Reacative.Domain;

namespace Reacative.Infrastructure.UI.ResourceDisplay
{
    public class ResourceDisplayController : BaseController<IResourceDisplayView>
    {
        private readonly Game _game;

        public ResourceDisplayController(Game game)
        {
            _game = game;
            _game.OnStateChanged += (oldState, newState) => View?.UpdateResources(newState.ResourceBankState.Energy, newState.ReactorState.Temperature);
        }
        protected override void OnAssign(IResourceDisplayView view)
        {
            view.UpdateResources(_game.CurrentState.ResourceBankState.Energy, _game.CurrentState.ReactorState.Temperature);
            SetActive(true);
        }
    }
}