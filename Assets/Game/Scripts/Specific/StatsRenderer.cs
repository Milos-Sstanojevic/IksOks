using TMPro;
using UnityEngine;

namespace Game
{
    public sealed class StatsRenderer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI totalGamesText;
        [SerializeField] private TextMeshProUGUI perPlayerWinText;
        [SerializeField] private TextMeshProUGUI numberOfDrawsText;
        [SerializeField] private TextMeshProUGUI averageDurationText;
        
        private StatsState _statsState;

        private void Awake()
        {
            _statsState=OR.Get<StatsState>();
        }

        private void OnEnable()
        {
            totalGamesText.text=_statsState.Current.totalGames.ToString();
            perPlayerWinText.text = $"{_statsState.Current.player1Wins}:{_statsState.Current.player2Wins}";
            numberOfDrawsText.text = _statsState.Current.draws.ToString();
            averageDurationText.text=TimeExtension.FormatTime(_statsState.Current.AverageDurationSeconds);
        }
    }
}