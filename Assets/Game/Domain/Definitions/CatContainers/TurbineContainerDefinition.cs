using System.Collections.Immutable;
using Reacative.Domain.State;

namespace Reacative.Domain.Definitions.CatContainers
{
    public class TurbineContainerDefinition : CatsContainerDefinition
    {
        protected override ICatsContainerState GetCatsContainer(GameState gameState)
        {
            return gameState.TurbineState;
        }

        protected override GameState SetActiveCats(GameState gameState, ImmutableList<string> cats)
        {
            return gameState with
            {
                TurbineState = gameState.TurbineState with
                {
                    ActiveCats = cats
                }
            };
        }
    }
}