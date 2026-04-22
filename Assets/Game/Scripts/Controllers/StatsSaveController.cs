using UnityEngine;

namespace Game
{
    public sealed class StatsSaveController : MonoBehaviour
    {
        private GameplayState _gameplayState;
        private StatsState _statsState;
        private GameHUDState _gameHUDState;
        
        private void Awake()
        {
            _gameplayState=OR.Get<GameplayState>();
            _statsState=OR.Get<StatsState>();
            _gameHUDState=OR.Get<GameHUDState>();
        }

        private void OnEnable()
        {
            _gameplayState.OnGameOver += OnGameOver;
        }

        private void OnDisable()
        {
            _gameplayState.OnGameOver -= OnGameOver;
        }

        private void OnGameOver(GameplayState.ResultType result)
        {
            var duration=_gameHUDState.DurationSeconds;
            switch (result)
            {
                case GameplayState.ResultType.XWon:
                    _statsState.RecordPlayer1Win(duration);
                    break;
                case GameplayState.ResultType.OWon:
                    _statsState.RecordPlayer2Win(duration);
                    break;
                case GameplayState.ResultType.Draw:
                    _statsState.RecordDraw(duration);
                    break;
            }
        }
    }
}