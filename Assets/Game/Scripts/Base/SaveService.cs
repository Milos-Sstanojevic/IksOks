using UnityEngine;

namespace Game
{
    public sealed class SaveService
    {
        private const float EnabledDb = 0f;
        private const float MutedDb = -80f;

        private const string ThemeKey = "theme.selected";
        private const string AudioMusicEnabledKey = "audio.music_enabled";
        private const string AudioSFXEnabledKey = "audio.sfx_enabled";
        private const string AudioMusicMixerDbKey = "audio.music_mixer_db";
        private const string AudioSfxMixerDbKey = "audio.sfx_mixer_db";
        
        private const string StatsTotalGamesKey = "stats.total_games";
        private const string StatsPlayer1WinsKey = "stats.player1_wins";
        private const string StatsPlayer2WinsKey = "stats.player2_wins";
        private const string StatsDrawsKey = "stats.draws";
        private const string StatsTotalDurationSecondsKey = "stats.total_duration_seconds";

        public ThemeState.ThemeId LoadTheme(ThemeState.ThemeId initialTheme)
        {
            if(!PlayerPrefs.HasKey(ThemeKey))
                return initialTheme;
            
            return (ThemeState.ThemeId) PlayerPrefs.GetInt(ThemeKey);
        }

        private void SaveTheme(ThemeState.ThemeId theme)
        {
            PlayerPrefs.SetInt(ThemeKey, (int) theme);
            PlayerPrefs.Save();
        }

        public bool LoadMusicEnabled(bool initialValue = true)
        {
            if (!PlayerPrefs.HasKey(AudioMusicEnabledKey))
                return initialValue;

            return PlayerPrefs.GetInt(AudioMusicEnabledKey) == 1;
        }

        public bool LoadSfxEnabled(bool initialValue = true)
        {
            if (!PlayerPrefs.HasKey(AudioSFXEnabledKey))
                return initialValue;

            return PlayerPrefs.GetInt(AudioSFXEnabledKey) == 1;
        }

        public float LoadMusicMixerDb(bool musicEnabled)
        {
            return PlayerPrefs.GetFloat(AudioMusicMixerDbKey, musicEnabled ? EnabledDb : MutedDb);
        }

        public float LoadSfxMixerDb(bool sfxEnabled)
        {
            return PlayerPrefs.GetFloat(AudioSfxMixerDbKey, sfxEnabled ? EnabledDb : MutedDb);
        }

        private void SaveMusicEnabled(bool enabled)
        {
            PlayerPrefs.SetInt(AudioMusicEnabledKey, enabled ? 1 : 0);
            PlayerPrefs.Save();
        }

        private void SaveSfxEnabled(bool enabled)
        {
            PlayerPrefs.SetInt(AudioSFXEnabledKey, enabled ? 1 : 0);
            PlayerPrefs.Save();
        }

        private void SaveMusicMixerDb(float valueDb)
        {
            PlayerPrefs.SetFloat(AudioMusicMixerDbKey, valueDb);
            PlayerPrefs.Save();
        }

        private void SaveSfxMixerDb(float valueDb)
        {
            PlayerPrefs.SetFloat(AudioSfxMixerDbKey, valueDb);
            PlayerPrefs.Save();
        }

        public StatsState.Data LoadStats()
        {
            return new StatsState.Data()
            {
                totalGames = PlayerPrefs.GetInt(StatsTotalGamesKey, 0),
                player1Wins = PlayerPrefs.GetInt(StatsPlayer1WinsKey, 0),
                player2Wins = PlayerPrefs.GetInt(StatsPlayer2WinsKey, 0),
                draws = PlayerPrefs.GetInt(StatsDrawsKey, 0),
                totalDurationSeconds = PlayerPrefs.GetFloat(StatsTotalDurationSecondsKey, 0f)
            };
        }

        private void SaveStats(StatsState.Data data)
        {
            PlayerPrefs.SetInt(StatsTotalGamesKey, data.totalGames);
            PlayerPrefs.SetInt(StatsPlayer1WinsKey, data.player1Wins);
            PlayerPrefs.SetInt(StatsPlayer2WinsKey, data.player2Wins);
            PlayerPrefs.SetInt(StatsDrawsKey, data.draws);
            PlayerPrefs.SetFloat(StatsTotalDurationSecondsKey, data.totalDurationSeconds);
            PlayerPrefs.Save();
        }

        public void Bind(ThemeState themeState)
        {
            themeState.OnChanged+=SaveTheme;
        }

        public void Bind(AudioSettingsState audioSettingsState)
        {
            audioSettingsState.OnMusicChanged += SaveMusicEnabled;
            audioSettingsState.OnSfxChanged += SaveSfxEnabled;
        }

        public void Bind(AudioService audioService)
        {
            audioService.OnMusicVolumeDbChanged += SaveMusicMixerDb;
            audioService.OnSfxVolumeDbChanged += SaveSfxMixerDb;
        }
        
        public void Bind(StatsState statsState)
        {
            statsState.OnChanged+=SaveStats;
        }
        
        public void Unbind(ThemeState themeState)
        {
            themeState.OnChanged-=SaveTheme;
        }

        public void Unbind(AudioSettingsState audioSettingsState)
        {
            audioSettingsState.OnMusicChanged -= SaveMusicEnabled;
            audioSettingsState.OnSfxChanged -= SaveSfxEnabled;
        }

        public void Unbind(AudioService audioService)
        {
            audioService.OnMusicVolumeDbChanged -= SaveMusicMixerDb;
            audioService.OnSfxVolumeDbChanged -= SaveSfxMixerDb;
        }

        public void Unbind(StatsState statsState)
        {
            statsState.OnChanged-=SaveStats;
        }
    }
}
