using Reacative.Domain.State;

namespace Reacative.Domain.CommandSystem
{
    public interface ICommand
    {
        public void Execute(Game game);
    }
}