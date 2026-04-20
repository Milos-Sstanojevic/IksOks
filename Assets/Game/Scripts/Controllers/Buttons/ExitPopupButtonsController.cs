using UnityEngine;

namespace Game
{
    public sealed class ExitPopupButtonsController : MonoBehaviour
    {
        public void Exit()
        {
            Application.Quit();
        }
    }
}