using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    public FitType fitType = FitType.Uniform;

    public int rows = 0;
    public int columns = 0;

    public Vector2 cellSize = Vector2.zero;
    public Vector2 spacing = Vector2.zero;

    public bool fitX = false;
    public bool fitY = false;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            fitX = true;
            fitY = true;

            float sqrt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrt);
            columns = Mathf.CeilToInt(sqrt);
        }
        else if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }
        else if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = (parentWidth / (float)columns) - (spacing.x / (float)columns) * (columns - 1) - (padding.left / (float)columns) - (padding.right / (float)columns);
        float cellHeight = (parentHeight / (float)rows) - (spacing.y / (float)rows) * (rows - 1) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        cellSize.x = fitX == true ? cellWidth : cellSize.x;
        cellSize.y = fitY == true ? cellHeight : cellSize.y;

        int rowCount = 0;
        int columnCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            RectTransform child = rectChildren[i];
            float posX = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            float posY = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(child, 0, posX, cellSize.x);
            SetChildAlongAxis(child, 1, posY, cellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }
}
