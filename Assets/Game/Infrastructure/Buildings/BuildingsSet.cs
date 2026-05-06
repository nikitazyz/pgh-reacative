using System;
using Reacative.Domain.State;

namespace Reacative.Infrastructure.Buildings
{
    public static class BuildingsSet
    {
        public enum BuildingType
        {
            Reactor,
            Cooler,
            Turbine,
            Lab
        }

        public static string IdFromType(BuildingType type)
        {
            return type switch
            {
                BuildingType.Reactor => ReactorState.ID,
                BuildingType.Cooler => CoolerState.ID,
                BuildingType.Turbine => TurbineState.ID,
                BuildingType.Lab => LabState.ID,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}