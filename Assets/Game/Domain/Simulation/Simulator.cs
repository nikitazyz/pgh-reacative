using System;
using System.Collections.Generic;
using System.Linq;
using Reacative.Domain.EventSystem;
using Reacative.Domain.State;

namespace Reacative.Domain.Simulation
{
    public class Simulator
    {
        private readonly IEventHandlerProvider _eventHandlerProvider;
        private readonly ISimulationSystem[] _simulationSystems;

        private readonly long _stepTime = 10000;
        public Simulator(IEventHandlerProvider eventHandlerProvider, IEnumerable<ISimulationSystem> simulationSystems)
        {
            _eventHandlerProvider = eventHandlerProvider;
            _simulationSystems = simulationSystems.ToArray();
        }

        public GameState Simulate(GameState gameState, long currentTime)
        {
            if (currentTime <= gameState.LastUpdateTime)
            {
                return gameState;
            }

            var deltaTime = currentTime - gameState.LastUpdateTime;
            var steps = deltaTime / _stepTime;
            var remainingTime = deltaTime % _stepTime;

            for (int i = 0; i < steps; i++)
            {
                gameState = StepSimulation(gameState, _stepTime);
            }

            if (remainingTime > 0)
            {
                gameState = StepSimulation(gameState, remainingTime);
            }

            return gameState with
            {
                LastUpdateTime = currentTime
            };
        }

        public GameState StepSimulation(GameState gameState, long stepTime)
        {
            foreach (var timelineEvent in gameState.EventTimeline.GetEventsBetween(gameState.LastUpdateTime, gameState.LastUpdateTime + stepTime))
            {
                gameState = SimulateUntil(gameState, timelineEvent.Timestamp);
                var handler = _eventHandlerProvider.GetHandlerForEvent(timelineEvent);
                gameState = handler?.Handle(timelineEvent, gameState) ?? gameState;
            }

            gameState = SimulateUntil(gameState, gameState.LastUpdateTime + stepTime);

            return gameState with
            {
                LastUpdateTime = gameState.LastUpdateTime + stepTime
            };
        }

        private GameState SimulateUntil(GameState gameState, long targetTime)
        {
            var deltaTime = (targetTime - gameState.LastUpdateTime) / 1000d;
            var simulationContext = new SimulationContext(deltaTime, targetTime);
            
            foreach (var system in _simulationSystems)
            {
                system.Simulate(gameState, simulationContext);
            }

            return gameState with
            {
                ReactorState = gameState.ReactorState with
                {
                    Temperature = gameState.ReactorState.Temperature + simulationContext.TemperatureDelta
                },
                ResourceBankState = gameState.ResourceBankState with
                {
                    Energy = gameState.ResourceBankState.Energy + simulationContext.ProducedEnergy
                },
                TurbineState = gameState.TurbineState with
                {
                    IsActive = !simulationContext.ShouldStopTurbine && gameState.TurbineState.IsActive
                },
                LastUpdateTime = targetTime
            };
        }
    }
}