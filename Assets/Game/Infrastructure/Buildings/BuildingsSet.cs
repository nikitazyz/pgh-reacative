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
            Lab,
            Specialist
        }

        public static string IdFromType(BuildingType type)
        {
            return type switch
            {
                BuildingType.Reactor => ReactorState.ID,
                BuildingType.Cooler => CoolerState.ID,
                BuildingType.Turbine => TurbineState.ID,
                BuildingType.Lab => LabState.ID,
                BuildingType.Specialist => SpecialistState.ID,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static BuildingType TypeFromId(string definitionKey)
        {
            if (definitionKey == ReactorState.ID)
            {
                return BuildingType.Reactor;
            }
            if (definitionKey == CoolerState.ID)
            {
                return BuildingType.Cooler;
            }
            if (definitionKey == TurbineState.ID)
            {
                return BuildingType.Turbine;
            }
            if (definitionKey == LabState.ID)
            {
                return BuildingType.Lab;
            }

            throw new ArgumentOutOfRangeException(nameof(definitionKey), definitionKey, null);
        }
    }
}