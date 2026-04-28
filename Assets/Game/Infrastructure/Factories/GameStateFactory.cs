using System.Collections.Generic;
using System.Collections.Immutable;
using Reacative.Domain.Cats;
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
                new List<string>().ToImmutableList()
            ),
            new TurbineState(
                0,
                false,
                0
            ),
            new CoolerState(
                0,
                new List<string>().ToImmutableList()
            ),
            new List<CatDefinition>().ToImmutableList(),
            new EventTimeline(new List<ITimelineEvent>().ToImmutableList()));
        }
    }
}