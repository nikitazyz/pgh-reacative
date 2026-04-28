using Reacative.Domain.State;

namespace Reacative.Domain.EventSystem
{
    public interface ITimelineEventHandler
    {
        public GameState Handle(ITimelineEvent timelineEvent, GameState gameState);
    }
}