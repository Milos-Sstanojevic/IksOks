using System;
using UnityEngine;

namespace Game
{
    public sealed class ThemeState
    {
        public enum ThemeId
        {
            RedBlack=0,
            BlueYellow=1,
            GreenPurple=2
        }
        
        private const string SaveKey = "selected_theme";
        
        public ThemeId SelectedTheme { get; private set; }

        public ThemeState()
        {
            Load();
        }

        public void SetTheme(ThemeId theme)
        {
            if (SelectedTheme == theme) return;

            SelectedTheme = theme;
            Save();
        }

        public void Load()
        {
            SelectedTheme=(ThemeId)PlayerPrefs.GetInt(SaveKey,(int)ThemeId.RedBlack);
        }

        private void Save()
        {
            PlayerPrefs.SetInt(SaveKey,(int)SelectedTheme);
            PlayerPrefs.Save();
        }
    }
}