using AiryUI;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public sealed class PanelView : MonoBehaviour
    {
        [SerializeField] private PanelSO panelConfig;
        [SerializeField] private AnimatedElement animatedElement;
        public AnimatedElement AnimatedElement =>animatedElement;
        public PanelSO Config =>panelConfig;

        private void Awake()
        {
            Assert.IsNotNull(panelConfig);
        }
    }
}