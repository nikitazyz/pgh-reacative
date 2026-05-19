using Reacative.Domain.State;

namespace Reacative.Domain.Cats
{
    public interface ICatNameGenerator
    {
        public string[] Generate(GameState gameState, int count);
    }
}