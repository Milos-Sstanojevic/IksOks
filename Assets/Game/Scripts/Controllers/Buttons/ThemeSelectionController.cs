using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public sealed class ThemeSelectionController : MonoBehaviour
    {
        [SerializeField] private List<ThemeElement> themeElements;
        private Transform _tr;
        private ThemeState _themeState;

        private void Awake()
        {
            Assert.IsNotNull(themeElements);
            Assert.IsTrue(themeElements.Count > 0);
            _themeState=OR.Get<ThemeState>();
            _tr=transform;
            for (int i = 0; i < themeElements.Count; i++)
                Instantiate(themeElements[i], _tr);
        }

        public void SetSelectedTheme(ThemeState.ThemeId themeId)
        {
            _themeState.SetTheme(themeId);
        }
    }
}