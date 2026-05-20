using System.Threading.Tasks;
using Reacative.Domain.State;

namespace Reacative.Domain.Cats
{
    public interface ICatNameGenerator
    {
        public Task<string[]> Generate(GameState gameState, int count);
    }
}