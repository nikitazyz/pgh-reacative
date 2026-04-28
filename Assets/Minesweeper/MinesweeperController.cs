using System;
using UnityEngine;

namespace Minesweeper
{
    public class MinesweeperController : MonoBehaviour
    {
        public event Action OnComplete;
        public event Action OnGameOver;
        
        [SerializeField] private Vector2Int _fieldSize = new Vector2Int(8, 8);
        [SerializeField] private int _minesCount = 5;
        
        [SerializeField] private MineCell _cellPrefab;

        private MineCell[,] _mineCells;
        
        private MinesweeperGame _currentGame;
        
        private bool _isPlaying;

        private void Start()
        {
            _mineCells = new MineCell[_fieldSize.x, _fieldSize.y];
            for (int i = 0; i < _fieldSize.x; i++)
            {
                for (int j = 0; j < _fieldSize.y; j++)
                {
                    _mineCells[i, j] = Instantiate(_cellPrefab, transform);
                    _mineCells[i, j].OnPrimaryClick += OnPrimary;
                    _mineCells[i, j].OnSecondaryClick += OnSecondary;
                }
            }
            
            StartGame();
        }

        private void OnPrimary(MinesweeperGame.Cell cell)
        {
            if (!_isPlaying)
            {
                return;
            }
            if (!_currentGame.OpenCell(cell.Position))
            {
                _isPlaying = false;
                for (int i = 0; i < _fieldSize.x; i++)
                {
                    for (int j = 0; j < _fieldSize.y; j++)
                    {
                        _mineCells[i, j].ShowMine();
                    }
                }
                OnGameOver?.Invoke();
                return;
            }

            UpdateCells();
            if (_currentGame.IsComplete())
            {
                _isPlaying = false;
                OnComplete?.Invoke();
            }
        }

        private void OnSecondary(MinesweeperGame.Cell cell)
        {
            if (!_isPlaying)
            {
                return;
            }
            _currentGame.FlagCell(cell.Position);
            UpdateCells();
        }

        private void UpdateCells()
        {
            for (int i = 0; i < _fieldSize.x; i++)
            {
                for (int j = 0; j < _fieldSize.y; j++)
                {
                    _mineCells[i, j].UpdateCell();
                }
            }
        }

        public void StartGame()
        {
            _currentGame = new MinesweeperGame(_fieldSize, _minesCount);
            var cells = _currentGame.Cells;
            for (int i = 0; i < _fieldSize.x; i++)
            {
                for (int j = 0; j < _fieldSize.y; j++)
                {
                    _mineCells[i, j].SetCell(cells[i, j]);
                    _mineCells[i, j].UpdateCell();
                }
            }
            _isPlaying = true;
        }
        
        
    }
}