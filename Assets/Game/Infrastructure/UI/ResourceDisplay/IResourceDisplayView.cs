namespace Reacative.Infrastructure.UI.ResourceDisplay
{
    public interface IResourceDisplayView : IUIView
    {
        public void UpdateResources(double energy, double temperature);
    }
}