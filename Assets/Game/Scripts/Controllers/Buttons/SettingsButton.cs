using UnityEngine;

namespace Game
{
    public sealed class SettingsButton : MonoBehaviour
    {
        [SerializeField] private PanelSO settingsPanel;

        private PanelState _state;
        private void Awake()
        {
            _state=OR.Get<PanelState>();
        }
        
        public void OpenSettingsPopup()
        {
            _state.Show(settingsPanel);
        }
    }
}