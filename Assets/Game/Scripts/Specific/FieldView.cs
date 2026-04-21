using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class FieldView : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        
        public int Index =>index;
        public Image Image =>image;

        private GameplayController _gameplayController;

        private void Awake()
        {
            _gameplayController = OR.Get<GameplayController>();
        }

        public void OnClick()
        {
            _gameplayController.TryPlay(index);
        }
    }
}