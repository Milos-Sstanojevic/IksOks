using UnityEngine;

namespace Game
{
    public static class TimeExtension
    {
        public static string FormatTime(float elapsedSeconds)
        {
            var totalSeconds = Mathf.FloorToInt(elapsedSeconds);
            var minutes = totalSeconds / 60;
            var seconds = totalSeconds % 60;
            return $"{minutes:00}:{seconds:00}";
        }
    }
}