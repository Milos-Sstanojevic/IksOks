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
        private readonly HashSet<PanelSO> _activeMainPanels = new();
        private PanelSO _activePopupPanel;

        public void Register(PanelView panelView)
        {
            Assert.IsNotNull(panelView);
            Assert.IsNotNull(panelView.Config);

            _panelMap[panelView.Config]=panelView.gameObject;
            panelView.gameObject.SetActive(false);
        }

        public GameObject GetPanelGO(PanelSO panel)
        {
            Assert.IsNotNull(panel);

            _panelMap.TryGetValue(panel, out var panelGO);
            return panelGO;
        }
        
        public void Show(PanelSO panel)
        {
            Assert.IsNotNull(panel);
            Assert.IsTrue(_panelMap.ContainsKey(panel), $"Panel not registered: {panel.name}");
            if (panel.isPopup)
            {
                SwitchPopup(panel);
                return;
            }

            ShowMain(panel);
           
        }

        private void SwitchPopup(PanelSO panel)
        {
            if (ReferenceEquals(_activePopupPanel, panel))
                return;

            var previous = _activePopupPanel;
            _activePopupPanel = panel;
            OnPopupChanged?.Invoke(previous, panel);
        }

        private void ShowMain(PanelSO panel)
        {
            if (!_activeMainPanels.Add(panel))
                return;

            OnMainChanged?.Invoke(null, panel);
        }

        public void ClosePopup()
        {
            if (_activePopupPanel == null) return;
            var previous=_activePopupPanel;
            _activePopupPanel = null;
            OnPopupChanged?.Invoke(previous,null);
        }

        public void Reset()
        {
            ClosePopup();

            if (_activeMainPanels.Count > 0)
            {
                var mainsToClose = new List<PanelSO>(_activeMainPanels);
                _activeMainPanels.Clear();

                for (int i = 0; i < mainsToClose.Count; i++)
                    OnMainChanged?.Invoke(mainsToClose[i], null);
            }

            _panelMap.Clear();
        }


    }
}
