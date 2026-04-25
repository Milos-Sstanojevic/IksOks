using System;

namespace Game
{
    public sealed class AudioSettingsState
    {
        public event Action<bool> OnMusicChanged;
        public event Action<bool> OnSfxChanged;

        private bool _musicEnabled;
        private bool _sfxEnabled;

        public AudioSettingsState(bool musicEnabled = true, bool sfxEnabled = true)
        {
            _musicEnabled = musicEnabled;
            _sfxEnabled = sfxEnabled;
        }
        
        public bool MusicEnabled => _musicEnabled;
        public bool SfxEnabled => _sfxEnabled;
        
        public void SetMusicEnabled(bool enabled)
        {
            if (_musicEnabled == enabled) return;
            
            _musicEnabled=enabled;
            OnMusicChanged?.Invoke(enabled);
        }
        
        public void SetSfxEnabled(bool enabled)
        {
            if (_sfxEnabled == enabled) return;
            
            _sfxEnabled=enabled;
            OnSfxChanged?.Invoke(enabled);
        }
    }
}
