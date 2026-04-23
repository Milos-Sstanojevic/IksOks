using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class FieldView : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        
        public Image Image =>image;

        private GameplayState _gameplayState;

        private void Awake()
        {
            _gameplayState = OR.Get<GameplayState>();
        }

        public void OnClick()
        {
            _gameplayState.TryPlay(index);
        }
    }
}