namespace Reacative.Infrastructure.UI
{
    public interface IUIView
    {
        public bool IsActive { get; }
        public void SetActive(bool active);
    }
}