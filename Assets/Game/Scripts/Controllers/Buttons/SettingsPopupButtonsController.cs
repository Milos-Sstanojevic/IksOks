using UnityEngine;
using UnityEngine.Assertions;
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
            Assert.IsNotNull(musicToggle);
            Assert.IsNotNull(sfxToggle);
            _audioSettingsState = OR.Get<AudioSettingsState>();
            SyncToggles();
        }

        private void OnEnable()
        {
            SyncToggles();
        }

        private void SyncToggles()
        {
            Assert.IsNotNull(_audioSettingsState);
            musicToggle.SetIsOnWithoutNotify(_audioSettingsState.MusicEnabled);
            sfxToggle.SetIsOnWithoutNotify(_audioSettingsState.SfxEnabled);
        }
        
        public void ToggleMusic()
        {
            _audioSettingsState.SetMusicEnabled(musicToggle.isOn);
        }

        public void ToggleSFX()
        {
            _audioSettingsState.SetSfxEnabled(sfxToggle.isOn);
        }
    }
}
