using System;
using Reacative.Domain.Calculators;
using Reacative.Domain.CommandSystem;
using Reacative.Domain.Configs;
using Reacative.Domain.Simulation;
using Reacative.Domain.State;
using Reacative.Domain.Time;

namespace Reacative.Domain
{
    public class Game
    {
        public event ChangeStateHandler OnStateChanged;
        
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
                new TurbineSimulation(configProvider.TurbineConfig),
                new GeneratorSimulation(configProvider.GeneratorConfig)
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
            if (oldState.Equals(_currentState))
            {
                return;
            }
            OnStateChanged?.Invoke(oldState, _currentState);
        }

        public Subscription Subscribe<T>(Func<GameState, T> property, Action<T> changeCallback)
        {
            var currentState = property(_currentState);
            
            ChangeStateHandler subscriptionCallback = (_, newState) =>
            {
                var next = property(newState);
                if (Equals(currentState, next))
                {
                    return;
                }
                
                currentState = next;
                changeCallback(next);
            };

            changeCallback(currentState);
            OnStateChanged += subscriptionCallback;

            return new Subscription(this, subscriptionCallback);
        }

        private void Unsubscribe(ChangeStateHandler callback)
        {
            OnStateChanged -= callback;
        }
        
        public class Subscription : IDisposable
        {
            private readonly Game _game;
            private readonly ChangeStateHandler _callback;
            public Subscription(Game game, ChangeStateHandler callback)
            {
                _game = game;
                _callback = callback;
            }

            public void Dispose()
            {
                _game.Unsubscribe(_callback);
            }
        }
        
        internal void SetState(GameState newState)
        {
            var oldState = _currentState;
            _currentState = newState;
            if (oldState.Equals(_currentState))
            {
                return;
            }
            OnStateChanged?.Invoke(oldState, _currentState);
        }

        public double GetMaxTemperature()
        {
            return ReactorCalculator.CalculateMaxTemperature(Config.ReactorConfig.MaxTemperature);
        }

        public bool IsOverheated()
        {
            return CurrentState.ReactorState.Temperature >=
                   GetMaxTemperature() * Config.ReactorConfig.OverheatThreshold;
        }

        public double GetTurbineReloadProgress()
        {
            var turbineState = CurrentState.TurbineState;
            var reactorState = CurrentState.ReactorState;
            if (!reactorState.IsActive || turbineState.IsActive)
            {
                return 0;
            }
            
            var turbineTime = CurrentState.TurbineState.ActivationTime;
            
            var chargeStartTime = turbineTime + (long)Config.TurbineConfig.TurbineTime * 1000;
            var currentTime = _timeProvider.GetTime();

            var fullChargeTime = (long)Config.TurbineConfig.ReloadTime * 1000;
            var timeFromChargeStart = currentTime - chargeStartTime;
            
            return Math.Clamp((double)timeFromChargeStart / fullChargeTime, 0.0, 1.0);
        }
    }
    
    public delegate void ChangeStateHandler(GameState oldState, GameState newState);
}