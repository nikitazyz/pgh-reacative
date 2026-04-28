namespace Reacative.Domain.EventSystem
{
    public interface IEventHandlerProvider
    {
        public ITimelineEventHandler GetHandlerForEvent(ITimelineEvent timelineEvent);
    }
}