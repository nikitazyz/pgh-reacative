namespace Reacative.Domain.CommandSystem
{
    public class TakeResourceCommand : ICommand, ICommandValidation
    {
        private readonly int _amount;

        public TakeResourceCommand(int amount)
        {
            _amount = amount;
        }

        public void Execute(Game game)
        {
            if (!IsValid(game))
            {
                return;
            }

            var energy = game.CurrentState.ResourceBankState.Energy;
            energy -= _amount;
            
            var newState = game.CurrentState with { ResourceBankState = game.CurrentState.ResourceBankState with{ Energy = energy } };
            game.SetState(newState);
        }

        public bool IsValid(Game game)
        {
            return game.CurrentState.ResourceBankState.Energy >= _amount;
        }
    }
}