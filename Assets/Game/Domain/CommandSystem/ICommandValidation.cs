namespace Reacative.Domain.CommandSystem
{
    public interface ICommandValidation
    {
        public bool IsValid(Game game);
    }
}