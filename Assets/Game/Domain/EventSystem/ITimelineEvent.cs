using System;

namespace Reacative.Domain.EventSystem
{
    public interface ITimelineEvent
    {
        public Guid Id { get
            {
                return Guid.NewGuid();
            } }
        public long Timestamp { get; }
        public object GetData();
    }

    public interface ITimelineEvent<T> : ITimelineEvent
    {
        public T Payload { get; }
    }
}