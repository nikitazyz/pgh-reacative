using System;
using Reacative.Domain.Calculators;
using Reacative.Domain.Configs;
using Reacative.Domain.State;

namespace Reacative.Domain.CommandSystem
{
    public class CoolCommand : ICommand
    {
        public void Execute(Game game)
        {
            game.Update();
            var gameState = game.CurrentState;

            var cool = CoolerCalculator.CalculateTemperatureDelta(
                gameState.ReactorState.Temperature,
                game.Config.ReactorConfig.LevelTemperatureMultiplier,
                gameState.ReactorState.Level,
                15,
                0.1,
                1
            );

            gameState = game.CurrentState with
            {
                ReactorState = gameState.ReactorState with
                {
                    Temperature = Math.Max(gameState.ReactorState.Temperature - cool, 0)
                }
            };
            game.SetState(gameState);
        }
    }
}