namespace Reacative.Domain.Calculators
{
    public static class TurbineCalculator
    {
        public static double CalculateEnergyBonus(double producedEnergy, int turbineLevel)
        {
            return producedEnergy * (1 + turbineLevel);
        }
    }
}