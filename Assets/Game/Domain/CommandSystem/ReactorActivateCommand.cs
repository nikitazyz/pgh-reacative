using Reacative.Domain.Calculators;

namespace Reacative.Domain.CommandSystem
{
    public class ReactorActivateCommand : ICommand
    {
        private readonly bool _isActive;

        public ReactorActivateCommand(bool isActive)
        {
            _isActive = isActive;
        }
        
        public void Execute(Game game)
        {
            game.Update();
            var reactorState = game.CurrentState.ReactorState.IsActive;
            if (reactorState == _isActive)
            {
                return;
            }

            var state = game.CurrentState;

            state = state with
            {
                ReactorState = state.ReactorState with
                {
                    IsActive = _isActive
                }
            };

            game.SetState(state);
        }
    }
}