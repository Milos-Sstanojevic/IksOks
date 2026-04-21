using UnityEngine;

namespace Game
{
    public sealed class GameplayController
    {
        private readonly BoardState _boardState;
        private readonly TurnState _turnState;

        private bool _isGameOver;
        private readonly int _lastLineIndex;
        private readonly int _boardSize;

        public GameplayController(BoardState boardState, TurnState turnState)
        {
            _boardState=boardState;
            _turnState=turnState;
            _boardSize=_boardState.OneDimension;
            _lastLineIndex=_boardSize - 1;
        }

        public void StartGame()
        {
            _isGameOver=false;
            _boardState.Reset();
            _turnState.Reset();
        }

        public void TryPlay(int index)
        {
            if(_isGameOver) return;
            
            var value=_turnState.GetCurrentValue();
            if (!_boardState.TryPlace(index, value)) return;

            if (HasWin(index,value))
            {
                _isGameOver=true;
                Debug.Log($"{value} WIN");
                return;
            }

            if (_boardState.IsFull())
            {
                _isGameOver=true;
                Debug.Log("DRAW");
                return;
            }
            
            _turnState.Next();
        }

        private bool HasWin(int lastIndex, BoardState.CellValue value)
        {
            if (value == BoardState.CellValue.Empty)
                return false;

            var row = lastIndex / _boardSize;
            var col = lastIndex % _boardSize;

            if (CheckRow(row, value)) return true;
            if (CheckColumn(col, value)) return true;
            if (row == col && CheckMainDiagonal(value)) return true;
            if (row + col == _lastLineIndex && CheckAntiDiagonal(value)) return true;

            return false;
        }

        private bool CheckRow(int row, BoardState.CellValue value)
        {
            var rowStart = row * _boardSize;

            for (var offset = 0; offset < _boardSize; offset++)
            {
                if (_boardState.Get(rowStart + offset) != value)
                    return false;
            }

            return true;
        }

        private bool CheckColumn(int col, BoardState.CellValue value)
        {
            for (var offset = 0; offset < _boardSize; offset++)
            {
                var index = col + offset * _boardSize;
                if (_boardState.Get(index) != value)
                    return false;
            }

            return true;
        }

        private bool CheckMainDiagonal(BoardState.CellValue value)
        {
            for (var offset = 0; offset < _boardSize; offset++)
            {
                var index = offset * _boardSize + offset;
                if (_boardState.Get(index) != value)
                    return false;
            }

            return true;
        }

        private bool CheckAntiDiagonal(BoardState.CellValue value)
        {
            for (var offset = 0; offset < _boardSize; offset++)
            {
                var index = offset * _boardSize + (_lastLineIndex - offset);
                if (_boardState.Get(index) != value)
                    return false;
            }

            return true;
        }
    }
}