using Reacative.Domain.Time;
using Reacative.Infrastructure.Services;

namespace Reacative.Infrastructure.Time
{
    public class TimeProvider : ITimeProvider, IService
    {
        public long GetTime()
        {
            return System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}