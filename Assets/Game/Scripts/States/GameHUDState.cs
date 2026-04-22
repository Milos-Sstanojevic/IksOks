using System;

namespace Game
{
    public sealed class GameHUDState
    {
        public event Action OnChanged;

        private float _durationSeconds;
        private int _numberOfMoves;
        public float DurationSeconds => _durationSeconds;
        public int MovesNumber => _numberOfMoves;

        public void Reset()
        {
            _durationSeconds=0;
            _numberOfMoves=0;
            OnChanged?.Invoke();
        }

        public void AddTime(float deltaTime)
        {
            _durationSeconds+=deltaTime;
            OnChanged?.Invoke();
        }
        
        public void RegisterMove()
        {
            _numberOfMoves++;
            OnChanged?.Invoke();
        }
    }
}