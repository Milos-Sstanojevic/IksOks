using UnityEngine;

namespace Game
{
    public sealed class AppInstall : MonoBehaviour
    {
        private void Awake()
        {
            OR.Init();
            
            OR.Set(new PanelState());
        }
    }
}