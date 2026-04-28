using System.Collections.Generic;

namespace Reacative.Domain.Utils
{
    public interface ITimeFunction
    {
        double Value(double time);

        IEnumerable<double> GetBreakpoints(double from, double to);

        bool TryIntegrate(double from, double to, out double result)
        {
            result = 0;
            return false;
        }
    }
}