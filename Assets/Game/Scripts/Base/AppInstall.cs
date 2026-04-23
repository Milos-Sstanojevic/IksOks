using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class AppInstall : MonoBehaviour
    {
        [SerializeField] private ThemeState.ThemeId initialTheme;
        [SerializeField] private AudioService audioService;
        [SerializeField] private AudioClip music;
        private static bool _installed;
        
        private void Awake()
        {
            if (_installed)
            {
                if (audioService != null)
                    Destroy(audioService.gameObject);
                Destroy(gameObject);
                return;
            }

            _installed = true;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(audioService.gameObject);
            
            OR.Init();
            
            var saveService=new SaveService();
            var themeState = new ThemeState(saveService.LoadTheme(initialTheme));
            var statsState = new StatsState(saveService.LoadStats());
            saveService.Bind(themeState);
            saveService.Bind(statsState);
            
            var audioSettingsState=new AudioSettingsState();
            OR.Set(audioSettingsState);
            audioService.Init(audioSettingsState);
            OR.Set(audioService);
            audioService.PlayMusic(music);
            
            OR.Set(saveService);
            OR.Set(themeState);
            OR.Set(statsState);
            OR.Set(new PanelState());
            var boardState=new BoardState();
            OR.Set(boardState);
            var turnState=new TurnState();
            OR.Set(turnState);
            OR.Set(new GameplayState(boardState, turnState));
            var sceneService=new SceneService();
            OR.Set(sceneService);
            OR.Set(new GameHUDState());

            
            if (SceneManager.GetActiveScene().name == "Bootstrap")
                sceneService.LoadMainMenu();
      
               
        }
    }
}
