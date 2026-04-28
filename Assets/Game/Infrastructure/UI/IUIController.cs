namespace Reacative.Infrastructure.UI
{
    public interface IUIController<T> where T : IUIView
    {
        public T View { get; }
        public bool IsActive { get; }
        
        public void Assign(T view);
        public void SetActive(bool active);
    }
}