using Reacative.Domain.Definitions.CatContainers;

namespace Reacative.Domain.CommandSystem
{
    public class SetCatOnBuildingCommand : ICommand, ICommandValidation
    {
        private readonly ICatsContainerDefinition _catsContainerDefinition;
        private readonly string _id;

        public SetCatOnBuildingCommand(ICatsContainerDefinition catsContainerDefinition, string id)
        {
            _catsContainerDefinition = catsContainerDefinition;
            _id = id;
        }

        public void Execute(Game game)
        {
            game.Update();
            if (!IsValid(game))
            {
                return;
            }

            var state = _catsContainerDefinition.SetCat(game.CurrentState, _id);
            game.SetState(state);
        }

        public bool IsValid(Game game)
        {
            return _catsContainerDefinition.CanSetCat(game.CurrentState, _id);
        }
    }
}