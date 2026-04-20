using UnityEngine;

namespace Game
{
    public sealed class MainMenuButtonsController : MonoBehaviour
    {
        [SerializeField] private PanelSO startPanel;
        [SerializeField] private PanelSO settingsPanel;
        [SerializeField] private PanelSO statsPanel;
        [SerializeField] private PanelSO exitPanel;
        
        private PanelState _state;

        private void Awake()
        {
            _state=OR.Get<PanelState>();
        }
        
        public void Play()
        {
            _state.Show(startPanel);
        }

        public void Settings()
        {
            _state.Show(settingsPanel);
        }

        public void Stats()
        {
            _state.Show(statsPanel);
        }

        public void Exit()
        {
            _state.Show(exitPanel);
        }
    }
}