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
            Assert.IsNotNull(panels);
            Assert.IsTrue(panels.Count > 0);
            
            for(int i=0;i<panels.Count;i++)
                _state.Register(panels[i]);
            
        }

        private void Start()
        {
            _state.Show(defaultPanelView.Config);
        }
        
        private void OnEnable()
        {
            _state.OnPopupChanged += Render;
            _state.OnMainChanged += Render;
        }

        private void Render(PanelSO previous, PanelSO current)
        {
            if(previous!=null)
                _state.GetPanelGO(previous)?.SetActive(false);
            if(current!=null)
                _state.GetPanelGO(current)?.SetActive(true);
        }
        
        private void OnDisable()
        {
            _state.OnMainChanged -= Render;
            _state.OnPopupChanged -= Render;
        }
    }
}