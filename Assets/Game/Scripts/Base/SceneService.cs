using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class SceneService
    {
        private readonly PanelState _state;
        private readonly GameplayState _gameplayState;
        private const string GameSceneName = "Game";
        private const string MainMenuName = "MainMenu";
        
        public SceneService()
        {
            _state=OR.Get<PanelState>();
            _gameplayState=OR.Get<GameplayState>();
        }

        public void LoadMainMenu()
        {
            _state.Reset();
            SceneManager.LoadScene(MainMenuName);
        }

        public void LoadGameScene()
        {
            _state.Reset();
            _gameplayState.StartGame();
            SceneManager.LoadScene(GameSceneName);
        }
    }
}