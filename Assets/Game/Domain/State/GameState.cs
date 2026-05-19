using System.Collections.Generic;
using System.Collections.Immutable;
using Reacative.Domain;
using Reacative.Domain.EventSystem;

namespace Reacative.Domain.State
{
    public record GameState(
        long LastUpdateTime,
        ResourceBankState ResourceBankState,
        ReactorState ReactorState,
        TurbineState TurbineState,
        CoolerState CoolerState,
        LabState LabState,
        GeneratorState GeneratorState,
        ImmutableList<CatState> GeneratedCats,
        ImmutableList<string> HiredCats,
        EventTimeline EventTimeline
    )
    {
        public override string ToString()
        {
            return $"Last Update Time: {System.DateTimeOffset.FromUnixTimeMilliseconds(LastUpdateTime)}\n"+
            $"Resources:\n{ResourceBankState.ToString().Replace("\n","\n\t")}\n"+
            $"Reactor:\n{ReactorState.ToString().Replace("\n","\n\t")}\n"+
            $"Cooler:{CoolerState.ToString().Replace("\n","\n\t")}\n"+
            $"Turbine:{TurbineState.ToString().Replace("\n","\n\t")}\n";
        }
    }
}