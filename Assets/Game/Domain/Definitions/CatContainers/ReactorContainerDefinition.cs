using System.Collections.Immutable;
using Reacative.Domain.State;

namespace Reacative.Domain.Definitions.CatContainers
{
    public class ReactorContainerDefinition : CatsContainerDefinition
    {
        protected override ICatsContainerState GetCatsContainer(GameState gameState)
        {
            return gameState.ReactorState;
        }

        protected override GameState SetActiveCats(GameState gameState, ImmutableList<string> cats)
        {
            return gameState with
            {
                ReactorState = gameState.ReactorState with
                {
                    ActiveCats = cats
                }
            };
        }
    }
}