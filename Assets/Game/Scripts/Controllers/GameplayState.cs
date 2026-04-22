using System;
using UnityEngine;

namespace Game
{
    public sealed class GameplayState
    {
        public enum ResultType
        {
            None=0,
            XWon=1,
            OWon=2,
            Draw=3
        }
        
        
        public event Action OnGameStarted;
        public event Action OnMovePlayed;
        public event Action<ResultType> OnGameOver;
        
        private readonly BoardState _boardState;
        private readonly TurnState _turnState;

        private bool _isGameOver;
        private readonly int _lastLineIndex;
        private readonly int _boardSize;
        private ResultType _result;
        public bool IsGameOver=>_isGameOver;
        public ResultType Result=>_result;

        public GameplayState(BoardState boardState, TurnState turnState)
        {
            _boardState=boardState;
            _turnState=turnState;
            _boardSize=_boardState.OneDimension;
            _lastLineIndex=_boardSize - 1;
        }


        public void StartGame()
        {
            _isGameOver=false;
            _result=ResultType.None;
            _boardState.Reset();
            _turnState.Reset();
            
            OnGameStarted?.Invoke();
        }

        public void TryPlay(int index)
        {
            if(_isGameOver) return;
            
            var value=_turnState.GetCurrentValue();
            if (!_boardState.TryPlace(index, value)) return;
            
            OnMovePlayed?.Invoke();
            
            if (HasWin(index,value))
            {
                _isGameOver=true;
                _result=value==BoardState.CellValue.X?ResultType.XWon:ResultType.OWon;
                OnGameOver?.Invoke(_result);
                return;
            }

            if (_boardState.IsFull())
            {
                _isGameOver=true;
                _result=ResultType.Draw;
                OnGameOver?.Invoke(_result);
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
