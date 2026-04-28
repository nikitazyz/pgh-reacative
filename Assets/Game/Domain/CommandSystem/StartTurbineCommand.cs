namespace Reacative.Domain.CommandSystem
{
    public class StartTurbineCommand : ICommand, ICommandValidation
    {
        
        public void Execute(Game game)
        {
            if (!IsValid(game))
                return;
            
            var turbineState = game.CurrentState.TurbineState;

            turbineState = turbineState with
            {
                IsActive = true,
                ActivationTime = game.Time.GetTime()
            };
            
            game.SetState(game.CurrentState with{ TurbineState = turbineState });
        }

        public bool IsValid(Game game)
        {
            var turbineState = game.CurrentState.TurbineState;
            var reactorState = game.CurrentState.ReactorState;
            if (!reactorState.IsActive || turbineState.IsActive)
            {
                return false;
            }
            
            var turbineTime = game.CurrentState.TurbineState.ActivationTime;
            long workTime = (long)(game.Config.TurbineConfig.ReloadTime + game.Config.TurbineConfig.TurbineTime) * 1000;
            var availableTime = turbineTime + workTime;
            
            if (game.Time.GetTime() < availableTime)
                return false;
            
            return true;
        }
    }
}