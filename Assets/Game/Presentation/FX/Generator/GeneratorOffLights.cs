using Reacative.Infrastructure;
using Reacative.Infrastructure.Services;
using UnityEngine;

namespace Reacative.Presentation.FX
{
    public class GeneratorOffLights : MonoBehaviour
    {
        private void Start()
        {
            var game = ServiceLocator.GetService<GameSession>().CurrentGame;

            game.Subscribe(s => s.GeneratorState.Power, p => gameObject.SetActive(p > 0));
        }
    }
}