using System;

namespace Game
{
    public sealed class TurnState
    {

        public enum PlayerTurn
        {
            X=0,
            O=1
        }
        
        public event Action OnTurnChanged;
        
        private PlayerTurn _currentTurn;
        public PlayerTurn CurrentTurn => _currentTurn;

        public void Reset()
        {
            _currentTurn=PlayerTurn.X;
            OnTurnChanged?.Invoke();
        }

        public void Next()
        {
            _currentTurn=_currentTurn==PlayerTurn.X?PlayerTurn.O:PlayerTurn.X;
            OnTurnChanged?.Invoke();
        }

        public BoardState.CellValue GetCurrentValue()
        {
            return _currentTurn==PlayerTurn.X?BoardState.CellValue.X:BoardState.CellValue.O;
        }
    }
}