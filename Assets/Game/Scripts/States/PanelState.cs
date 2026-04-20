using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public sealed class PanelState
    {
        public event Action<PanelSO,PanelSO> OnMainChanged;
        public event Action<PanelSO, PanelSO> OnPopupChanged;

        private readonly Dictionary<PanelSO, GameObject> _panelMap = new();
        private PanelSO _activePopupPanel;
        private PanelSO _activeMainPanel;

        public void Register(PanelView panelView)
        {
            Assert.IsNotNull(panelView);
            
            _panelMap.Add(panelView.Config, panelView.gameObject);
            panelView.gameObject.SetActive(false);
        }

        public GameObject GetPanelGO(PanelSO panel)
        {
            Assert.IsNotNull(panel);
            
            return _panelMap[panel];
        }
        
        public void Show(PanelSO panel)
        {
            Assert.IsNotNull(panel);
            if (panel.isPopup)
            {
                SwitchPopup(panel);
                return;
            }

            SwitchMain(panel);
          
        }

        private void SwitchPopup(PanelSO panel)
        {
            var previous = _activePopupPanel;
            _activePopupPanel = panel;
            OnPopupChanged?.Invoke(previous, panel);
        }

        private void SwitchMain(PanelSO panel)
        {  
            var previous = _activeMainPanel;
            _activeMainPanel = panel;
            OnMainChanged?.Invoke(previous, panel);
        }

        public void ClosePopup()
        {
            if (_activePopupPanel == null) return;
            var previous=_activePopupPanel;
            _activePopupPanel = null;
            OnPopupChanged?.Invoke(previous,null);
        }

        private void CloseMain()
        {
            if (_activeMainPanel == null) return;
            var previous=_activeMainPanel;
            _activeMainPanel = null;
            OnPopupChanged?.Invoke(previous,null);

        }

        public void Reset()
        {
            ClosePopup();
            CloseMain();
        }
        
       
    }
}