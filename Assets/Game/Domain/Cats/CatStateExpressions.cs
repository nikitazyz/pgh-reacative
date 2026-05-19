using Reacative.Domain.State;

namespace Reacative.Domain.Cats
{
    public static class CatStateExpressions
    {
        public static bool IsCatHired(this GameState gameState, CatState catState)
        {
            return gameState.HiredCats.Contains(catState.Name);
        }
    }
}