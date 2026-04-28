using System;
using Reacative.Domain.CommandSystem;
using Reacative.Domain.Configs;
using Reacative.Domain.Simulation;
using Reacative.Domain.State;
using Reacative.Domain.Time;

namespace Reacative.Domain
{
    public class Game
    {
        public event Action<GameState,GameState> OnStateChanged;
        
        private GameState _currentState;
        private readonly Simulator _simulator;
        private readonly ITimeProvider _timeProvider;

        public GameState CurrentState => _currentState;

        public IConfigProvider Config { get; private set; }

        public ITimeProvider Time => _timeProvider;

        public Game(GameState state, IConfigProvider configProvider, ITimeProvider timeProvider)
        {
            _currentState = state;
            Config = configProvider;
            var simulationSystems = new ISimulationSystem[]
            {
                new ReactorSimulation(configProvider.ReactorConfig),
                new TurbineSimulation(configProvider.TurbineConfig)
            };
            _simulator = new Simulator(null, simulationSystems);
            _timeProvider = timeProvider;
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute(this);
        }

        public void Update()
        {
            var oldState = _currentState;
            _currentState = _simulator.Simulate(_currentState, _timeProvider.GetTime());
            OnStateChanged?.Invoke(oldState, _currentState);
        }
        
        public void SetState(GameState newState)
        {
            _currentState = newState;
            Update();
        }
    }
}