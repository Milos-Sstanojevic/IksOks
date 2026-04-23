using UnityEngine;

namespace Game
{
    public sealed class SettingsPopupButtonsController : MonoBehaviour
    {
        private AudioSettingsState _audioSettingsState;

        private void Awake()
        {
            _audioSettingsState = OR.Get<AudioSettingsState>();
        }
        
        public void ToggleMusic(bool enabled)
        {
            _audioSettingsState.SetMusicEnabled(enabled);
        }

        public void ToggleSFX(bool enabled)
        {
            _audioSettingsState.SetSFXEnabled(enabled);
        }
    }
}