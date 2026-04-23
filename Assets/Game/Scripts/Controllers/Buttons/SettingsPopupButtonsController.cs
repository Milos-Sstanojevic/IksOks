using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class SettingsPopupButtonsController : MonoBehaviour
    {
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle sfxToggle;
        private AudioSettingsState _audioSettingsState;

        private void Awake()
        {
            _audioSettingsState = OR.Get<AudioSettingsState>();
        }
        
        public void ToggleMusic()
        {
            _audioSettingsState.SetMusicEnabled(musicToggle.isOn);
        }

        public void ToggleSFX()
        {
            _audioSettingsState.SetSFXEnabled(sfxToggle.isOn);
        }
    }
}