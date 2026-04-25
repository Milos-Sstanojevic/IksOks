using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class FlexibleGridLayout : LayoutGroup
    {
        [SerializeField] private int rows;
        [SerializeField] private int columns;
        [SerializeField] private Vector2 cellSize;
        [SerializeField] private Vector2 spacing;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
        }

        public override void CalculateLayoutInputVertical()
        {
        }

        public override void SetLayoutHorizontal()
        {
            ApplyLayout();
        }

        public override void SetLayoutVertical()
        {
            ApplyLayout();
        }

        private void ApplyLayout()
        {
            base.CalculateLayoutInputHorizontal();

            if (rectChildren.Count == 0)
            {
                rows = 0;
                columns = 0;
                cellSize = Vector2.zero;
                return;
            }

            float squareRoot = Mathf.Sqrt(rectChildren.Count);
            rows = Mathf.CeilToInt(squareRoot);
            columns = Mathf.CeilToInt(squareRoot);

            float availableWidth = rectTransform.rect.width - padding.horizontal;
            float availableHeight = rectTransform.rect.height - padding.vertical;

            float maxCellWidth = (availableWidth - (spacing.x * (columns - 1))) / columns;
            float maxCellHeight = (availableHeight - (spacing.y * (rows - 1))) / rows;
            float squareCellSize = Mathf.Max(0f, Mathf.Min(maxCellWidth, maxCellHeight));

            cellSize = new Vector2(squareCellSize, squareCellSize);

            float requiredWidth = (squareCellSize * columns) + (spacing.x * (columns - 1));
            float requiredHeight = (squareCellSize * rows) + (spacing.y * (rows - 1));

            float startX = GetStartOffset(0, requiredWidth);
            float startY = GetStartOffset(1, requiredHeight);

            for (int i = 0; i < rectChildren.Count; i++)
            {
                int row = i / columns;
                int column = i % columns;
                RectTransform item = rectChildren[i];

                float xPos = startX + (squareCellSize + spacing.x) * column;
                float yPos = startY + (squareCellSize + spacing.y) * row;

                SetChildAlongAxis(item, 0, xPos, squareCellSize);
                SetChildAlongAxis(item, 1, yPos, squareCellSize);
            }
        }
    }
}
