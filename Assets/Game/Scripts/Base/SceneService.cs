using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class SceneService
    {
        private readonly PanelState _state;
        private const string GameSceneName = "Game";
        private const string MainMenuName = "MainMenu";
        
        public SceneService()
        {
            _state=OR.Get<PanelState>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MainMenuName);
        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene(GameSceneName);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _state.Reset();
        }
    }
}