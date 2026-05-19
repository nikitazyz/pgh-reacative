using System.Collections.Generic;
using Reacative.Domain.State;

namespace Reacative.Domain.CommandSystem
{
    public class AddGeneratedCatsCommand : ICommand
    {
        private readonly IEnumerable<CatState> _states;

        public AddGeneratedCatsCommand(IEnumerable<CatState> states)
        {
            _states = states;
        }

        public void Execute(Game game)
        {
            var state = game.CurrentState;

            state = state with
            {
                GeneratedCats = state.GeneratedCats.AddRange(_states)
            };
            
            game.SetState(state);
        }
    }
}