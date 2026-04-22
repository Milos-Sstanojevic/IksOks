using UnityEngine;

namespace Game
{
    public sealed class SaveService
    {
        private const string ThemeKey = "theme.selected";
        
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

        public void SaveTheme(ThemeState.ThemeId theme)
        {
            PlayerPrefs.SetInt(ThemeKey, (int) theme);
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

        public void SaveStats(StatsState.Data data)
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
        
        public void Bind(StatsState statsState)
        {
            statsState.OnChanged+=SaveStats;
        }
        
        public void Unbind(ThemeState themeState)
        {
            themeState.OnChanged-=SaveTheme;
        }

        public void Unbind(StatsState statsState)
        {
            statsState.OnChanged-=SaveStats;
        }
    }
}