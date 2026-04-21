using System;
using UnityEngine.Assertions;

namespace Game
{
    public sealed class BoardState
    {
        public enum CellValue
        {
            Empty=0,
            X=1,
            O=2
        }
        
        public event Action<int> OnCellChanged;
        public event Action OnReset;

        private readonly CellValue[] _cells = new CellValue[9];
        public int OneDimension=>(int)Math.Sqrt(_cells.Length);
        public bool TryPlace(int index, CellValue value)
        {
            Assert.IsTrue(index>=0 && index<9);
            Assert.IsTrue(value!=CellValue.Empty);

            if (_cells[index] != CellValue.Empty)
                return false;
            _cells[index] = value;
            OnCellChanged?.Invoke(index);
            return true;
        }

        public CellValue Get(int index)
        {
            Assert.IsTrue(index>=0 && index<9);
            return _cells[index];
        }

        public bool IsFull()
        {
            for (int i = 0; i < _cells.Length; i++)
                if (_cells[i] == CellValue.Empty)
                    return false;
            return true;
        }

        public void Reset()
        {
            for(int i=0;i<_cells.Length;i++)
                _cells[i]=CellValue.Empty;
            OnReset?.Invoke();
        }
    }
}