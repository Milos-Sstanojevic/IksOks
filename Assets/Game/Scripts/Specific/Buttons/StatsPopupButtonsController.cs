using UnityEngine;

namespace Game
{
    public sealed class StatsPopupButtonsController : MonoBehaviour
    {
        private PanelState _state;

        private void Awake()
        {
            _state=OR.Get<PanelState>();
        }
    }
}