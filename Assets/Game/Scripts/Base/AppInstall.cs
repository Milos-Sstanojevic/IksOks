using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class AppInstall : MonoBehaviour
    {
        private static bool _installed;
        
        private void Awake()
        {
            if (_installed)
            {
                Destroy(gameObject);
                return;
            }

            _installed = true;
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene("Bootstrap", LoadSceneMode.Single);
            
            OR.Init();
            
            OR.Set(new PanelState());
            var sceneService=new SceneService();
            OR.Set(sceneService);
            OR.Set(new ThemeState());
            var boardState=new BoardState();
            OR.Set(boardState);
            var turnState=new TurnState();
            OR.Set(turnState);
            OR.Set(new GameplayController(boardState, turnState));

            if (SceneManager.GetActiveScene().name == "Bootstrap")
                sceneService.LoadMainMenu();
      
               
        }
    }
}