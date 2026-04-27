using System.Runtime.InteropServices;
using UnityEngine;

namespace Game
{
    public sealed class ExitPopupButtonsController : MonoBehaviour
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void ReloadItchPage();
#endif

        public void Exit()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            ReloadItchPage();
#else
            Application.Quit();
#endif
        }
    }
}
