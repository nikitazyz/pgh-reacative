using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Minesweeper
{
    public class MineCell : MonoBehaviour, IPointerUpHandler
    {
        public event Action<MinesweeperGame.Cell> OnPrimaryClick;
        public event Action<MinesweeperGame.Cell> OnSecondaryClick;
        
        [SerializeField] private Image _background;

        [SerializeField] private Sprite _closedCell;
        [SerializeField] private Sprite _openedCell;
        [SerializeField] private Sprite[] _numbers;
        
        [SerializeField] private Sprite _mine;
        [SerializeField] private Sprite _flag;

        private MinesweeperGame.Cell _cell;
        
        public void SetCell(MinesweeperGame.Cell cell)
        {
            _cell = cell;
            UpdateCell();
        }

        public void UpdateCell()
        {
            _background.sprite = _cell.IsOpened ? _openedCell : _closedCell;

            _background.sprite = _closedCell;
            if (_cell.IsFlagged)
            {
                _background.sprite = _flag;
            }

            if (_cell.IsOpened)
            {
                _background.sprite = _openedCell;
                if (_cell.MinesCount > 0)
                {
                    _background.sprite = _numbers[_cell.MinesCount-1];
                }
            }
        }

        public void ShowMine()
        {
            if (!_cell.IsMine)  return;
            _background.sprite = _mine;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("OnPointerUp");
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    OnPrimaryClick?.Invoke(_cell);
                    break;
                case PointerEventData.InputButton.Right:
                    OnSecondaryClick?.Invoke(_cell);
                    break;
            }
        }
    }
}