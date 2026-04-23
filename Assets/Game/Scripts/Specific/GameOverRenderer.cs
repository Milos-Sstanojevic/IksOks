using System;
using TMPro;
using UnityEngine;

namespace Game
{
    public sealed class GameOverRenderer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI winnerText;
        [SerializeField] private TextMeshProUGUI durationText;
        
        private GameplayState _gameplayState;
        private GameHUDState _gameHUDState;

        private void Awake()
        {
            _gameplayState=OR.Get<GameplayState>();
            _gameHUDState=OR.Get<GameHUDState>();
        }

        private void OnEnable()
        {
            GameOver();
        }
        
        private void GameOver()
        {
            switch (_gameplayState.Result)
            {
                case GameplayState.ResultType.XWon:
                    winnerText.text = "X Win!";
                    break;
                case GameplayState.ResultType.OWon:
                    winnerText.text = "O Win!";
                    break;
                case GameplayState.ResultType.Draw:
                    winnerText.text = "Draw!";
                    break;
            }
            durationText.text=TimeExtension.FormatTime(_gameHUDState.DurationSeconds);
        }
    }
}