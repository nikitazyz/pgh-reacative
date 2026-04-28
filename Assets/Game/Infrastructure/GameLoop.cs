using System.Collections;
using Reacative.Domain;
using UnityEngine;

namespace Reacative.Infrastructure
{
    public class GameLoop : MonoBehaviour
    {
        private Game _game;
        private float _refreshInterval;
        public void Init(Game game, float refreshInterval = 1)
        {
            _game = game;
            _refreshInterval = refreshInterval;
        }

        void Start()
        {
            StartCoroutine(GameLoopRoutine());
        }

        private IEnumerator GameLoopRoutine()
        {
            while (true)
            {
                _game.Update();
                Debug.Log(_game.CurrentState);
                yield return new WaitForSeconds(_refreshInterval);
            }
        }
    }
}