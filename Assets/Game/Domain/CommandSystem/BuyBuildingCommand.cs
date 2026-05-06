using Reacative.Domain.Definitions;

namespace Reacative.Domain.CommandSystem
{
    public class BuyBuildingCommand : ICommand, ICommandValidation
    {
        private readonly IPurchasableBuildingDefinition _definition;

        public BuyBuildingCommand(IPurchasableBuildingDefinition definition)
        {
            _definition = definition;
        }

        public void Execute(Game game)
        {
            if (!IsValid(game))
            {
                return;
            }

            var energy = game.CurrentState.ResourceBankState.Energy;
            energy -= _definition.Cost;
            
            var newState = game.CurrentState with { ResourceBankState = game.CurrentState.ResourceBankState with{ Energy = energy } };
            
            game.SetState(_definition.GetBoughtState(newState));
        }
        
        public bool IsValid(Game game)
        {
            return game.CurrentState.ResourceBankState.Energy >= _definition.Cost;
        }
    }
}