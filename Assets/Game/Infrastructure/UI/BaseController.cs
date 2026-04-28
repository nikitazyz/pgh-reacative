namespace Reacative.Infrastructure.UI
{
    public abstract class BaseController<T> : IUIController<T> where T : IUIView
    {
        public T View { get; private set; }
        public bool IsActive { get; private  set; }
        public void Assign(T view)
        {
            View = view;
            OnAssign(view);
        }
        
        protected virtual void OnAssign(T view) {}

        public void SetActive(bool active)
        {
            View.SetActive(active);
            IsActive = active;
        }
    }
}