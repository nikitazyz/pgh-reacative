using System.Collections.Generic;
using System.Collections.Immutable;
using Reacative.Domain.EventSystem;
using Reacative.Domain.State;

namespace Reacative.Infrastructure.Factories
{
    public static class GameStateFactory
    {
        public static GameState InitialGameState(long currentTime, bool isReactorActiveAtStart = false)
        {
            return new GameState(currentTime, 
            new ResourceBankState(100, 0, 0),
            new ReactorState(
                0,
                0,
                isReactorActiveAtStart,
                ImmutableList<string>.Empty
            ),
            new TurbineState(
                0,
                false,
                0,
                false
            ),
            new CoolerState(
                0,
                ImmutableList<string>.Empty,
                false
            ),
            new LabState(false),
            new GeneratorState(1, ImmutableList<string>.Empty),
            ImmutableList<CatState>.Empty,
            ImmutableList<string>.Empty,
            new EventTimeline(ImmutableList<ITimelineEvent>.Empty));
        }
    }
}