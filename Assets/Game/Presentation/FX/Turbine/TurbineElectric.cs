using Reacative.Domain;
using Reacative.Infrastructure;
using Reacative.Infrastructure.Services;
using UnityEngine;

namespace Reacative.Presentation
{
    public class TurbineElectric : MonoBehaviour
    {
        [SerializeField] private GameObject _turbineElectric;
        private Game _game;
        void Start()
        {
            _game = ServiceLocator.GetService<GameSession>().CurrentGame;
            _game.Subscribe(s => s.TurbineState.IsActive, OnActiveChange);
        }

        private void OnActiveChange(bool obj)
        {
            _turbineElectric.SetActive(obj);
        }
    }
}
