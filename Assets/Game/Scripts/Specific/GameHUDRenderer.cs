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
            timeText.text = FormatTime(_hudState.DurationSeconds);
            movesText.text = _hudState.MovesNumber.ToString();
        }

        private static string FormatTime(float elapsedSeconds)
        {
            var totalSeconds = Mathf.FloorToInt(elapsedSeconds);
            var minutes = totalSeconds / 60;
            var seconds = totalSeconds % 60;
            return $"{minutes:00}:{seconds:00}";
        }
    }
}