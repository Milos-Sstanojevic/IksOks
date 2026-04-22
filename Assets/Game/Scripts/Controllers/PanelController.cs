using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public sealed class PanelController : MonoBehaviour
    {
        [SerializeField] private PanelView defaultPanelView;
        [SerializeField] private List<PanelView> panels;

        private PanelState _state;

        private void Awake()
        {
            _state = OR.Get<PanelState>();
            Assert.IsNotNull(_state);
            Assert.IsNotNull(defaultPanelView);
            Assert.IsNotNull(defaultPanelView.Config);
            Assert.IsFalse(defaultPanelView.Config.isPopup);
            Assert.IsNotNull(panels);
            Assert.IsTrue(panels.Count > 0);
            
            for(int i=0;i<panels.Count;i++)
                _state.Register(panels[i]);
            
        }

        private void Start()
        {
            _state.Show(defaultPanelView.Config);

            for (int i = 0; i < panels.Count; i++)
            {
                if (panels[i].Config.isPopup)
                    continue;

                _state.Show(panels[i].Config);
            }
        }
        
        private void OnEnable()
        {
            _state.OnPopupChanged += Render;
            _state.OnMainChanged += Render;
        }

        private void Render(PanelSO previous, PanelSO current)
        {
            if (previous != null)
            {
                var previousGO = _state.GetPanelGO(previous);
                if (previousGO != null)
                    previousGO.SetActive(false);
            }

            if (current != null)
            {
                var currentGO = _state.GetPanelGO(current);
                if (currentGO != null)
                    currentGO.SetActive(true);
            }
        }
        
        private void OnDisable()
        {
            _state.OnMainChanged -= Render;
            _state.OnPopupChanged -= Render;
        }
    }
}
