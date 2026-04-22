using System;
using UnityEngine;

namespace Game
{
    public sealed class GameOverController : MonoBehaviour
    {
        [SerializeField] private PanelSO gameOverPanel;
        
        private GameplayState _gameplayState;
        private PanelState _panelState;
        
        private void Awake()
        {
            _gameplayState=OR.Get<GameplayState>();
            _panelState=OR.Get<PanelState>();
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
            _panelState.Show(gameOverPanel);
        }
    }
}