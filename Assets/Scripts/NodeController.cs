using System.Collections.Generic;
using UnityEngine;

public class NodeController : Singleton<NodeController>
{
    [SerializeField] private LayerMask nodeLayer;

    private Node selectedNode = null;

    private Camera cam;

    protected override void Awake()
    {
        base.Awake();

        cam = Camera.main;
    }
    private void Update()
    {
        Vector3 mouseWorld = cam.ScreenToWorldPoint(InputHandler.Instance.MousePosition);
        mouseWorld.z = 0f;

        if(InputHandler.Instance.MouseLeftClickHold && selectedNode != null)
        {
            selectedNode.transform.position = SnapToGrid(mouseWorld);
        }
        if(InputHandler.Instance.MouseLeftClickPressed)
        {
            TrySelectNode(mouseWorld);
        }
        if(InputHandler.Instance.MouseLeftClickReleased && selectedNode != null)
        {
            selectedNode = null;
        }
    }

    private void TrySelectNode(Vector3 mouseWorld)
    {
        Collider2D hit = Physics2D.OverlapPoint(mouseWorld, nodeLayer);
        if(hit != null)
        {
            selectedNode = hit.GetComponent<Node>();
        }
    }
    private Vector3 SnapToGrid(Vector3 position)
    {
        float cellSize = GridDrawer.Instance.CellSize;
        float x = Mathf.Round(position.x / cellSize) * cellSize;
        float y = Mathf.Round(position.y / cellSize) * cellSize;
        return new Vector3(x, y, 0f);
    }
}
