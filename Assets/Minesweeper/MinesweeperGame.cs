using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Minesweeper
{
    public class MinesweeperGame
    {
        private readonly Vector2Int _fieldSize;
        private readonly int _mineCount;

        private Cell[,] _cells;
        
        public Cell[,] Cells => _cells;

        public MinesweeperGame(Vector2Int fieldSize,  int mineCount)
        {
            _mineCount = mineCount;
            _fieldSize = fieldSize;
            Generate();
        }

        private void Generate()
        {
            _cells = new Cell[_fieldSize.x, _fieldSize.y];
            int counter = 0;
            while (counter < _mineCount)
            {
                
                var x = Random.Range(0, _fieldSize.x);
                var y = Random.Range(0, _fieldSize.y);
                
                if (_cells[x, y] != null) continue;
                
                var mineCell = new Cell(new Vector2Int(x,y));
                _cells[x, y] = mineCell;
                counter++;
            }

            for (int i = 0; i < _fieldSize.x; i++)
            {
                for (int j = 0; j < _fieldSize.y; j++)
                {
                    if (_cells[i, j] != null)  continue;
                    var position = new Vector2Int(i, j);
                    var minesCount = GetMineCount(position);
                    var cell = new Cell(position, minesCount);
                    _cells[i, j] = cell;
                }
            }
            
            
        }

        private int GetMineCount(Vector2Int position)
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int x = position.x + i-1;
                    int y = position.y + j-1;
                    if (x < 0 || x >= _fieldSize.x || y < 0 || y >= _fieldSize.y)
                    {
                        continue;
                    }
                    
                    var cell = _cells[x, y];
                    if (cell is { IsMine: true })
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public bool OpenCell(Vector2Int position)
        {
            var cell = _cells[position.x, position.y];
            if (cell.IsFlagged) return true;
            if (cell.IsOpened) return true;
            if (cell.IsMine) return false;

            RecursiveOpenCell(position);
            return true;
        }

        public bool IsComplete()
        {
            return _cells.Cast<Cell>().All(cell => cell.IsMine || cell.IsOpened);
        }

        public void FlagCell(Vector2Int position)
        {
            var cell = _cells[position.x, position.y];
            if (cell.IsOpened) return;
            
            cell.SetFlag(!cell.IsFlagged);
        }

        public Vector2Int[] GetMinePositions()
        {
            var minePositions = new List<Vector2Int>(_mineCount);
            for (int i = 0; i < _fieldSize.x; i++)
            {
                for (int j = 0; j < _fieldSize.y; j++)
                {
                    minePositions.Add(new Vector2Int(i, j));
                }
            }
            return minePositions.ToArray();
        }

        private void RecursiveOpenCell(Vector2Int position)
        {
            var cell = _cells[position.x, position.y];
            if (cell.IsFlagged) return;
            if (cell.IsMine) return;
            if (cell.IsOpened) return;

            cell.Open();
            if (cell.MinesCount != 0) return;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int x = position.x + i-1;
                    int y = position.y + j-1;
                    if (x < 0 || x >= _fieldSize.x || y < 0 || y >= _fieldSize.y)
                    {
                        continue;
                    }
                        
                    RecursiveOpenCell(new Vector2Int(x, y));
                }
            }
        }
        
        public class Cell
        {
            public Vector2Int Position { get; }
            public bool IsMine { get; }
            public bool IsOpened { get; private set; }
            public bool IsFlagged { get; private set; }
            public int MinesCount { get; private set; }

            public Cell(Vector2Int position)
            {
                Position = position;
                IsMine = true;
            }
            
            public Cell(Vector2Int position, int minesCount)
            {
                Position = position;
                IsMine = false;
                MinesCount = minesCount;
            }

            public void SetFlag(bool flag)
            {
                if (IsOpened)
                {
                    return;
                }
                IsFlagged = flag;
            }

            public void Open()
            {
                IsOpened = true;
            }
        }
    }

    
}
