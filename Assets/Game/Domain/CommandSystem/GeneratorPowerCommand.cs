using System;

namespace Reacative.Domain.CommandSystem
{
    public class GeneratorPowerCommand : ICommand
    {
        public void Execute(Game game)
        {
            game.Update();
            var state = game.CurrentState;
            var config = game.Config;

            var newState = state with
            {
                GeneratorState = state.GeneratorState with
                {
                    Power = Math.Min(1, state.GeneratorState.Power + config.GeneratorConfig.PowerIncreaseSpeed * 0.01)
                }
            };
            
            game.SetState(newState);
        }
    }
}