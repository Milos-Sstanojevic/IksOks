using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public sealed class PanelView : MonoBehaviour
    {
        [SerializeField] private PanelSO panelConfig;
        public PanelSO Config =>panelConfig;

        private void Awake()
        {
            Assert.IsNotNull(panelConfig);
        }
    }
}