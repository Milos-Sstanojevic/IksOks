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

        public override void CalculateLayoutInputVertical()
        {
        }

        public override void SetLayoutHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            float sqrRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrRt);
            columns = Mathf.CeilToInt(sqrRt);
            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;
            float cellWidth = parentWidth / columns - ((spacing.x / columns) * 2) - (padding.left / (float)columns) -
                              (padding.right / (float)columns);
            float cellHeight = parentHeight / rows - ((spacing.y / rows) * 2) - (padding.top / (float)rows) -
                               (padding.bottom / (float)rows);

            cellSize.x = cellWidth;
            cellSize.y = cellHeight;

            int columnCount = 0;
            int rowCount = 0;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                rowCount = i / columns;
                columnCount = i % columns;

                var item = rectChildren[i];
                var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
                var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;
                SetChildAlongAxis(item, 0, xPos, cellSize.x);
                SetChildAlongAxis(item, 1, yPos, cellSize.y);
            }
        }

        public override void SetLayoutVertical()
        {
        }
    }
}