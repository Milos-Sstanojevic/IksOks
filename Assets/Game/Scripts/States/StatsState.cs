using System;

namespace Game
{
    public sealed class StatsState
    {
        [Serializable]
        public struct Data
        {
            public int totalGames;
            public int player1Wins;
            public int player2Wins;
            public int draws;
            public float totalDurationSeconds;
            public float AverageDurationSeconds=> totalGames <= 0 ? 0f : totalDurationSeconds / totalGames;
        }

        public event Action<Data> OnChanged;
        
        private Data _data;
        public Data Current => _data;

        public StatsState(Data initialData)
        {
            _data = initialData;
        }

        public void RecordPlayer1Win(float durationSeconds)
        {
            var data=_data;
            data.player1Wins++;
            data.totalGames++;
            data.totalDurationSeconds+=durationSeconds;
            _data=data;
            Set(data);
        }

        public void RecordPlayer2Win(float durationSeconds)
        {
            var data=_data;
            data.player2Wins++;
            data.totalGames++;
            data.totalDurationSeconds+=durationSeconds;
            _data=data;
            Set(data);
        }

        public void RecordDraw(float durationSeconds)
        {
            var data=_data;
            data.draws++;
            data.totalGames++;
            data.totalDurationSeconds+=durationSeconds;
            _data=data;
            Set(data);
        }

        private void Set(Data data)
        {
            _data = data;
            OnChanged?.Invoke(data);
        }

        public void Reset()
        {
            _data = default;
            OnChanged?.Invoke(default);
        }
        
    }
}