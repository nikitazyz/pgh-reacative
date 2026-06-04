namespace Reacative.Infrastructure.InteractionSystem
{
    public interface IInteractable
    {
        public string InteractionPrompt { get; }
        void Interact(Interaction interaction);
    }
}