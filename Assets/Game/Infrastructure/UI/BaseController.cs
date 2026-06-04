namespace Reacative.Infrastructure.UI
{
    public abstract class BaseController<T> : IUIController<T> where T : IUIView
    {
        public T View { get; private set; }
        public bool IsActive => View.IsActive;
        public void Assign(T view)
        {
            View = view;
            OnAssign(view);
            View.SetActive(IsActive);
        }
        
        protected virtual void OnAssign(T view) {}

        public void SetActive(bool active)
        {
            View.SetActive(active);
            OnSetActive(active);
        }
        
        protected virtual void OnSetActive(bool active) { }
    }
}