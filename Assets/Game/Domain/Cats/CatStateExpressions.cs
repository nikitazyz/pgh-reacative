using System.Linq;
using Reacative.Domain.State;

namespace Reacative.Domain.Cats
{
    public static class CatStateExpressions
    {
        public static bool IsCatHired(this GameState gameState, CatState catState)
        {
            return gameState.HiredCats.Contains(catState.Id);
        }

        public static CatState[] GetHiredCats(this GameState gameState)
        {
            return gameState.GeneratedCats.Where(c => IsCatHired(gameState, c)).ToArray();
        }

        public static CatState[] GetAvailableCats(this GameState gameState)
        {
            return gameState.GeneratedCats.Where(c => !IsCatHired(gameState, c)).ToArray();
        }

        public static CatState GetCatById(this GameState gameState, string id)
        {
            return gameState.GeneratedCats.FirstOrDefault(c => c.Id == id);
        }
    }
}