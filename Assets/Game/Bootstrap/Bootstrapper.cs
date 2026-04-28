using Reacative.Domain;
using Reacative.Infrastructure;
using Reacative.Infrastructure.Configs;
using Reacative.Infrastructure.Factories;
using Reacative.Infrastructure.Services;
using Reacative.Infrastructure.Time;
using Reacative.Infrastructure.UI.ResourceDisplay;
using Reacative.Presentation.Configs;
using UnityEngine;

namespace Reacative.Bootstrap
{
    public static class Bootstrapper
    {
        public static void SystemsInit()
        {
            var timeProvider = new TimeProvider();
            var config = LoadConfig();
            var uiConfig = LoadUIConfig();

            ServiceLocator.RegisterService(config);
            ServiceLocator.RegisterService(uiConfig);
            ServiceLocator.RegisterService(new GameSession(timeProvider, config));
            ServiceLocator.RegisterService(timeProvider);
        }

        public static void GameInit()
        {
            GameSession gameSession = ServiceLocator.GetService<GameSession>();
            TimeProvider timeProvider = ServiceLocator.GetService<TimeProvider>();
            UIConfig uiConfig = ServiceLocator.GetService<UIConfig>();
            
            gameSession.StartNewSession(GameStateFactory.InitialGameState(timeProvider.GetTime()));
            GameObject gameObject = new GameObject
            {
                name = "GameLoop"
            };
            //GameObject.DontDestroyOnLoad(gameObject);
            var gameLoop = gameObject.AddComponent<GameLoop>();
            gameLoop.Init(gameSession.CurrentGame);
            
            SetupUI(gameSession.CurrentGame, uiConfig);

            Debug.Log("Game initialized");
        }

        public static void SetupUI(Game game, UIConfig config)
        {
            var resourceController = new ResourceDisplayController(game);
            var ui = Object.Instantiate(config.ResourceDisplay);
            resourceController.Assign(ui);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BeforeSceneLoad()
        {
            SystemsInit();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AfterSceneLoad()
        {
            GameInit();
        }

        private static GameConfig LoadConfig()
        {
            return Resources.Load<GameConfig>("GameConfig");
        }

        private static UIConfig LoadUIConfig()
        {
            return Resources.Load<UIConfig>("UIConfig");
        }
    }
}