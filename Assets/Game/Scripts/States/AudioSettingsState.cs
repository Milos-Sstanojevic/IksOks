using System;

namespace Game
{
    public sealed class AudioSettingsState
    {
        public event Action<bool> OnMusicChanged;
        public event Action<bool> OnSFXChanged;

        private bool _musicEnabled=true;
        private bool _sfxEnabled=true;
        
        public bool MusicEnabled => _musicEnabled;
        public bool SFXEnabled => _sfxEnabled;
        
        public void SetMusicEnabled(bool enabled)
        {
            if (_musicEnabled == enabled) return;
            
            _musicEnabled=enabled;
            OnMusicChanged?.Invoke(enabled);
        }
        
        public void SetSFXEnabled(bool enabled)
        {
            if (_sfxEnabled == enabled) return;
            
            _sfxEnabled=enabled;
            OnSFXChanged?.Invoke(enabled);
        }
    }
}