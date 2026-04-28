using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Reacative.Domain.EventSystem
{
    public record EventTimeline(
        ImmutableList<ITimelineEvent> Events
    )
    {
        public IReadOnlyCollection<ITimelineEvent> GetEvents() => Events;

        public ITimelineEvent GetLastEvent() => Events.Count > 0 ? Events[^1] : null;

        internal IEnumerable<ITimelineEvent> GetEventsBetween(double lastUpdateTime, long currentTime)
        {
            foreach (var e in Events)
            {
                if (e.Timestamp > lastUpdateTime && e.Timestamp < currentTime)
                {
                    yield return e;
                }
            }
        }

        public class Builder
        {
            private readonly List<ITimelineEvent> _events = new();

            public Builder WithEvent(ITimelineEvent timelineEvent)
            {
                _events.Add(timelineEvent);
                return this;
            }

            public Builder WithEvents(IEnumerable<ITimelineEvent> timelineEvents)
            {
                _events.AddRange(timelineEvents);
                return this;
            }

            public Builder WithEvents(params ITimelineEvent[] timelineEvents)
            {
                _events.AddRange(timelineEvents);
                return this;
            }

            public Builder ClearEvents()
            {
                _events.Clear();
                return this;
            }

            public Builder RemoveEvent(ITimelineEvent timelineEvent)
            {
                _events.Remove(timelineEvent);
                return this;
            }

            public Builder RemoveOutdatedEvents(long cutoffTime)
            {
                _events.RemoveAll(e => e.Timestamp < cutoffTime);
                return this;
            }

            public EventTimeline Build() => new(_events.ToImmutableList());
        }
    }
}