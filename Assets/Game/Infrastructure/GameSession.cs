using Reacative.Domain;
using Reacative.Domain.Configs;
using Reacative.Domain.State;
using Reacative.Domain.Time;
using Reacative.Infrastructure.Services;

namespace Reacative.Infrastructure
{
    public class GameSession : IService
    {
        private Game _currentGame;
        private ITimeProvider _timeProvider;
        private IConfigProvider _configProvider;

        public Game CurrentGame => _currentGame;
        public GameSession(ITimeProvider timeProvider, IConfigProvider config)
        {
            _timeProvider = timeProvider;
            _configProvider = config;
        }

        public Game StartNewSession(GameState state)
        {
            _currentGame = new Game(state, _configProvider, _timeProvider);
            return _currentGame;
        }
    }
}