using System;

namespace Reacative.Domain.Calculators
{
    public static class ReactorCalculator
    {
        public static double CalculateEnergyProduction(double baseProduction, double time, double levelProductionMultiplier, int level)
        {
            return baseProduction * time + levelProductionMultiplier * level;
        }

        public static double CalculateOverheatEnergyProduction(double baseProduction, double overheatMultiplier)
        {
            return baseProduction * overheatMultiplier;
        }

        public static double CalculateTemperatureIncrease(double currentTemperature, double baseTemperatureIncrease, double time, double levelTemperatureMultiplier, int level, double maxTemperature)
        {
            return Math.Min(currentTemperature + baseTemperatureIncrease * time + levelTemperatureMultiplier * level, maxTemperature);
        }

        internal static double CalculateTemperatureDecrease(double temperature, double deltaTime, double baseTemperatureIncrease, double levelTemperatureMultiplier, int level)
        {
            return Math.Max(0, temperature - (baseTemperatureIncrease * deltaTime + levelTemperatureMultiplier * level));
        }
    }
}