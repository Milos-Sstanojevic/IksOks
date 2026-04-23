using System;
using TMPro;
using UnityEngine;

namespace Game
{
    public sealed class GameOverRenderer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI winnerText;
        [SerializeField] private TextMeshProUGUI durationText;
        [SerializeField] private AudioClip gameOverAudio;
        
        private GameplayState _gameplayState;
        private GameHUDState _gameHUDState;
        private AudioService _audioService;
        private void Awake()
        {
            _gameplayState=OR.Get<GameplayState>();
            _gameHUDState=OR.Get<GameHUDState>();
            _audioService=OR.Get<AudioService>();
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
            _audioService.PlaySfx(gameOverAudio);
            durationText.text=TimeExtension.FormatTime(_gameHUDState.DurationSeconds);
        }
    }
}