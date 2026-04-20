using UnityEngine;

namespace Game
{
    public sealed class StartGamePopupButtonsController : MonoBehaviour
    {
        private SceneService _sceneService;

        private void Awake()
        {
            _sceneService=OR.Get<SceneService>();
        }

        public void StartGame()
        {
            _sceneService.LoadGameScene();
        }
    }
}