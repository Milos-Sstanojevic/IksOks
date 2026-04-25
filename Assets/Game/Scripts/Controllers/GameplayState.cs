using System;

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
        private readonly int _boardCellCount;
        private int _winningCellsMask;
        private ResultType _result;
        public bool IsGameOver=>_isGameOver;
        public ResultType Result=>_result;

        public GameplayState(BoardState boardState, TurnState turnState)
        {
            _boardState=boardState;
            _turnState=turnState;
            _boardSize=_boardState.OneDimension;
            _boardCellCount=_boardSize * _boardSize;
            _lastLineIndex=_boardSize - 1;
        }


        public void StartGame()
        {
            _isGameOver=false;
            _result=ResultType.None;
            _winningCellsMask=0;
            _boardState.Reset();
            _turnState.Reset();
            
            OnGameStarted?.Invoke();
        }

        public bool IsWinningCell(int index)
        {
            if ((uint)index >= (uint)_boardCellCount)
                return false;

            return (_winningCellsMask & (1 << index)) != 0;
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
            _winningCellsMask=0;

            if (value == BoardState.CellValue.Empty)
                return false;

            var row = lastIndex / _boardSize;
            var col = lastIndex % _boardSize;

            _winningCellsMask |= GetRowMask(row, value);
            _winningCellsMask |= GetColumnMask(col, value);

            if (row == col)
                _winningCellsMask |= GetMainDiagonalMask(value);

            if (row + col == _lastLineIndex)
                _winningCellsMask |= GetAntiDiagonalMask(value);

            return _winningCellsMask != 0;
        }

        private int GetRowMask(int row, BoardState.CellValue value)
        {
            var rowStart = row * _boardSize;
            var mask = 0;

            for (var offset = 0; offset < _boardSize; offset++)
            {
                var index = rowStart + offset;
                if (_boardState.Get(index) != value)
                    return 0;

                mask |= 1 << index;
            }

            return mask;
        }

        private int GetColumnMask(int col, BoardState.CellValue value)
        {
            var mask = 0;

            for (var offset = 0; offset < _boardSize; offset++)
            {
                var index = col + offset * _boardSize;
                if (_boardState.Get(index) != value)
                    return 0;

                mask |= 1 << index;
            }

            return mask;
        }

        private int GetMainDiagonalMask(BoardState.CellValue value)
        {
            var mask = 0;

            for (var offset = 0; offset < _boardSize; offset++)
            {
                var index = offset * _boardSize + offset;
                if (_boardState.Get(index) != value)
                    return 0;

                mask |= 1 << index;
            }

            return mask;
        }

        private int GetAntiDiagonalMask(BoardState.CellValue value)
        {
            var mask = 0;

            for (var offset = 0; offset < _boardSize; offset++)
            {
                var index = offset * _boardSize + (_lastLineIndex - offset);
                if (_boardState.Get(index) != value)
                    return 0;

                mask |= 1 << index;
            }

            return mask;
        }
    }
}
