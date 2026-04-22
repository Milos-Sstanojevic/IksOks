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
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if(scene.name==GameSceneName)
                _gameplayState.StartGame();
        }
        
        public void LoadMainMenu()
        {
            _state.Reset();
            SceneManager.LoadScene(MainMenuName);
        }

        public void LoadGameScene()
        {
            _state.Reset();
            SceneManager.LoadScene(GameSceneName);
            // _gameplayState.StartGame();
        }
    }
}
