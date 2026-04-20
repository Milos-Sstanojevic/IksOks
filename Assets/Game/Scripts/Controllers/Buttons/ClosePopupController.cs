using UnityEngine;

namespace Game
{
    public sealed class ClosePopupController : MonoBehaviour
    {
        private PanelState _state;
        private void Awake()
        {
            _state=OR.Get<PanelState>();
        }

        public void Close()
        {
            _state.ClosePopup();    
        }
    }
}