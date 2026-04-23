using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public sealed class GameHUDRenderer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI movesText;

        private GameHUDState _hudState;

        private void Awake()    
        {
            Assert.IsNotNull(timeText);
            Assert.IsNotNull(movesText);   
            _hudState=OR.Get<GameHUDState>();    
        }

        private void OnEnable()
        {
            _hudState.OnChanged += Render;
            Render();
        }

        private void OnDisable()
        {
            _hudState.OnChanged -= Render;
        }

        private void Render()
        {
            timeText.text = TimeExtension.FormatTime(_hudState.DurationSeconds);
            movesText.text = _hudState.MovesNumber.ToString();
        }

       
    }
}