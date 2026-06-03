using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper
{
    public class GameStatus : MonoBehaviour
    {
        [SerializeField] private Image _gameStatusImage;
        [SerializeField] private Sprite _startGame;
        [SerializeField] private Sprite _gameOver;
        [SerializeField] private Sprite _winGame;

        public void SetStatus(bool? status)
        {
            if (!status.HasValue)
            {
                _gameStatusImage.sprite = _startGame;
                return;
            }

            _gameStatusImage.sprite = status.Value ? _winGame : _gameOver;
        }
    }
}