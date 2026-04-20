using UnityEngine;

namespace Game
{
    public sealed class ThemeElement : MonoBehaviour
    {
        [SerializeField] private ThemeState.ThemeId themeId;
        private ThemeSelectionController _themeSelectionController;
        public ThemeState.ThemeId ThemeId =>themeId;

        private void Awake()
        {
            _themeSelectionController=GetComponentInParent<ThemeSelectionController>();
        }
        
        public void SetTheme()
        {
            _themeSelectionController.SetSelectedTheme(themeId);
        }
    }
}