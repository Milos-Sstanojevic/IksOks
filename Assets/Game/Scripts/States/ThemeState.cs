using System;
using UnityEngine;

namespace Game
{
    public sealed class ThemeState
    {
        private const string SaveKey = "selected_theme";
        public enum ThemeId
        {
            RedBlack=0,
            BlueYellow=1,
            GreenPurple=2
        }

        public event Action<ThemeId> OnChanged;
        private ThemeId _selectedTheme;
        public ThemeId SelectedTheme => _selectedTheme;
        
        public ThemeState(ThemeId selectedTheme)
        {
            _selectedTheme = selectedTheme;
        }

        public void SetTheme(ThemeId theme)
        {
            _selectedTheme = theme;
            OnChanged?.Invoke(theme);
        }
    }
}