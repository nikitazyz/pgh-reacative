using System.Collections.Generic;
using Reacative.Domain.State;

namespace Reacative.Domain.Definitions.CatContainers
{
    public interface ICatsContainerDefinition
    {
        public GameState SetCat(GameState gameState, string id);
        public GameState RemoveCat(GameState gameState, string id);
        public bool CanSetCat(GameState gameState, string id);
        public bool CanRemoveCat(GameState gameState, string id);
        IEnumerable<string> GetActiveCats(GameState gameState);
    }
}