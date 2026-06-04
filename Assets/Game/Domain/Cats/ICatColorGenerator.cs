using Reacative.Domain.State;

namespace Reacative.Domain.Cats
{
    public interface ICatColorGenerator
    {
        public CatColor[] GenerateColor(GameState gameState, int count);
    }
}