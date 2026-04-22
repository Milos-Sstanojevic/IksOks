using UnityEngine;

namespace Game
{
    public sealed class GameHUDController : MonoBehaviour
    {
        private GameplayState _gameplayState;
        private GameHUDState _gameHUDState;

        private void Awake()
        {
            _gameplayState=OR.Get<GameplayState>();
            _gameHUDState=OR.Get<GameHUDState>();
        }

        private void OnEnable()
        {
            _gameplayState.OnGameStarted += OnGameStarted;
            _gameplayState.OnMovePlayed += OnMovePlayed;
        }

        private void OnDisable()
        {
            _gameplayState.OnGameStarted -= OnGameStarted;
            _gameplayState.OnMovePlayed -= OnMovePlayed;
        }

        private void OnGameStarted()
        {
            _gameHUDState.Reset();
        }

        private void OnMovePlayed()
        {
            _gameHUDState.RegisterMove();
        }
    }
}