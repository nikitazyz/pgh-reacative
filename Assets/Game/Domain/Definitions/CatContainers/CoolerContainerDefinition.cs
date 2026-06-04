using System.Collections.Immutable;
using Reacative.Domain.State;

namespace Reacative.Domain.Definitions.CatContainers
{
    public class CoolerContainerDefinition : CatsContainerDefinition
    {
        protected override ICatsContainerState GetCatsContainer(GameState gameState)
        {
            return gameState.CoolerState;
        }

        protected override GameState SetActiveCats(GameState gameState, ImmutableList<string> cats)
        {
            return gameState with
            {
                CoolerState = gameState.CoolerState with
                {
                    ActiveCats = cats
                }
            };
        }
    }
}