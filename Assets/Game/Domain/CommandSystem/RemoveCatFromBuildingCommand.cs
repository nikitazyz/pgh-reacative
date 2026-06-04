using Reacative.Domain.Definitions.CatContainers;

namespace Reacative.Domain.CommandSystem
{
    public class RemoveCatFromBuildingCommand : ICommand, ICommandValidation
    {
        private readonly ICatsContainerDefinition _definition;
        private readonly string _id;

        public RemoveCatFromBuildingCommand(ICatsContainerDefinition definition, string id)
        {
            _definition = definition;
            _id = id;
        }

        public void Execute(Game game)
        {
            game.Update();
            if (!IsValid(game))
            {
                return;
            }

            var state = _definition.RemoveCat(game.CurrentState, _id);
            game.SetState(state);
        }

        public bool IsValid(Game game)
        {
            return _definition.CanRemoveCat(game.CurrentState, _id);
        }
    }
}