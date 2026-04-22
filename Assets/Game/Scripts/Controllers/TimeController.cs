using UnityEngine;

namespace Game
{
    public sealed class TimeController : MonoBehaviour
    {
        private GameplayState _gameplayState;
        private GameHUDState _gameHUDState;

        private void Awake()
        {
            _gameplayState=OR.Get<GameplayState>();
            _gameHUDState=OR.Get<GameHUDState>();
        }

        private void Update()
        {
            if (_gameplayState.IsGameOver)
                return;
            
            _gameHUDState.AddTime(Time.deltaTime);
        }
    }
}