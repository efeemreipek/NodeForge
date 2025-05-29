using UnityEngine;

public class GridDrawer : Singleton<GridDrawer>
{
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private int width = 50;
    [SerializeField] private int height = 50;
    [SerializeField] private float lineWidth = 0.05f;
    [SerializeField] private Material lineMaterial;
    [SerializeField] private Color lineColor = Color.black;

    public float CellSize => cellSize;

    private void Start()
    {
        DrawGrid();
    }

    [ContextMenu("Clear Grid")]
    private void ClearGrid()
    {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
    [ContextMenu("Draw Grid")]
    private void DrawGrid()
    {
        ClearGrid();

        Vector3 offset = new Vector3(width * cellSize, height * cellSize, 0) * 0.5f;

        for(int x = 0; x <= width; x++)
        {
            float xPos = x * cellSize - offset.x;
            CreateLine(
                new Vector3(xPos, -offset.y, 0),
                new Vector3(xPos, height * cellSize - offset.y, 0)
            );
        }

        for(int y = 0; y <= height; y++)
        {
            float yPos = y * cellSize - offset.y;
            CreateLine(
                new Vector3(-offset.x, yPos, 0),
                new Vector3(width * cellSize - offset.x, yPos, 0)
            );
        }
    }
    private void CreateLine(Vector3 start, Vector3 end)
    {
        GameObject lineObj = new GameObject("GridLine");
        lineObj.transform.parent = transform;

        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.material = lineMaterial;
        lr.startColor = lineColor;
        lr.endColor = lineColor;
        lr.useWorldSpace = true;
        lr.sortingOrder = -2;
    }
}
