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
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _minesCount;

        [SerializeField] private Sprite _closedCell;
        [SerializeField] private Sprite _openedCell;
        
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
            _icon.sprite = _cell.IsFlagged ? _flag : null;
            _icon.color = _cell.IsFlagged ? Color.white : new Color(0,0,0,0);
            _minesCount.text = _cell.IsOpened && _cell.MinesCount > 0 ? _cell.MinesCount.ToString() : "";
        }

        public void ShowMine()
        {
            if (!_cell.IsMine)  return;
            _background.sprite = _openedCell;
            _icon.sprite = _mine;
            _icon.color = Color.white;
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