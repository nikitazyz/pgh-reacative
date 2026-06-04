using Reacative.Domain;
using Reacative.Infrastructure;
using Reacative.Infrastructure.Services;
using UnityEngine;
using UnityEngine.Rendering;

namespace Reacative.Presentation.FX
{
    public class OverheatVolume : MonoBehaviour
    {
        [SerializeField] private Volume _volume;

        private Game _game;
        private void Start()
        {
            _game = ServiceLocator.GetService<GameSession>().CurrentGame;
        }

        private void Update()
        {
            if (_game.IsOverheated())
            {
                _volume.weight = Mathf.MoveTowards(_volume.weight, 1f, Time.deltaTime);
            }
            else
            {
                _volume.weight = Mathf.MoveTowards(_volume.weight, 0, Time.deltaTime);
            }
        }
    }
}