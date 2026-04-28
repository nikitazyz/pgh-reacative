namespace Reacative.Domain.Calculators
{
    public static class CoolerCalculator
    {
        public static double CalculateTemperatureDelta(double currentTemperature, double heatMultiplier, int reactorLevel, double coolDampingMultiplier, double coolMultiplier, int coolerLevel)
        {
            return (currentTemperature + heatMultiplier * reactorLevel) / coolDampingMultiplier * (1 + coolMultiplier * coolerLevel);
        }
    }
}