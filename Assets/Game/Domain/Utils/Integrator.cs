using System;
using System.Collections.Generic;
using System.Linq;

namespace Reacative.Domain.Utils
{
    public class Integrator
    {
        public static double Integrate(double from, double to, List<ITimeFunction> functions)
        {
            var points = new SortedSet<double> { from, to };

            foreach (var function in functions)
            {
                points.UnionWith(function.GetBreakpoints(from, to));
            }

            var ordered = points.ToArray();

            double result = 0;

            for (int i = 0; i < ordered.Length - 1; i++)
            {
                double start = ordered[i];
                double end = ordered[i + 1];

                if (TryIntegrateSegment(start, end, functions, out double segmentResult))
                {
                    result += segmentResult;
                }
                else
                {
                    Func<double, double> combinedFunction = t =>
                    {
                        double rate = 0;
                        foreach (var f in functions)
                        {
                            rate *= f.Value(t);
                        }
                        return rate;
                    };

                    result += AdaptiveIntegrate(start, end, combinedFunction);
                }
            }

            return result;
        }

        private static bool TryIntegrateSegment(double from, double to, List<ITimeFunction> functions, out double result)
        {
            result = 0;

            if (functions.Count == 0)
            {
                return true;
            }

            if (functions.Count == 1)
            {
                return functions[0].TryIntegrate(from, to, out result);
            }

            bool constant = true;

            foreach (var f in functions)
            {
                if (Math.Abs(f.Value(from) - f.Value(to)) > 1e-6)
                {
                    constant = false;
                    break;
                }
            }

            if (constant)
            {
                double value = functions[0].Value(from);
                result = value * (to - from);
                return true;
            }

            return false;
        }

        private static double AdaptiveIntegrate(double from, double to, Func<double, double> function, double tolerance = 1e-4)
        {
            static double mid(double a, double b) => (a + b) / 2;

            double integrateSegment(double a, double b)
            {
                double fa = function(a);
                double fb = function(b);
                double fm = function(mid(a, b));

                return (b - a) * (fa + 4 * fm + fb) / 6;
            }

            double recursiveIntegrate(double a, double b)
            {
                double I1 = integrateSegment(a, b);
                double m = mid(a, b);
                double I2 = integrateSegment(a, m) + integrateSegment(m, b);

                if (Math.Abs(I1 - I2) < tolerance)
                {
                    return I2;
                }
                return recursiveIntegrate(a, m) + recursiveIntegrate(m, b);
            }

            return recursiveIntegrate(from, to);
        }
    }
}