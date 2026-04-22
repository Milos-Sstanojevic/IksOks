using UnityEngine;

namespace Game
{
    public sealed class GameOverButtonsController : MonoBehaviour
    {
        private SceneService _sceneService;
        private GameplayState _gameplayState;
        private PanelState _panelState;
        private void Awake()
        {
            _sceneService=OR.Get<SceneService>();
            _gameplayState=OR.Get<GameplayState>();
            _panelState=OR.Get<PanelState>();
        }
        
        public void MainMenu()
        {
            _sceneService.LoadMainMenu();
        }

        public void PlayAgain()
        {
            _panelState.ClosePopup();
            _gameplayState.StartGame();
        }
    }
}
