using System.Collections.Generic;
using UnityEngine;

public class NodeController : Singleton<NodeController>
{
    [SerializeField] private LayerMask nodeLayer;
    [SerializeField] private LayerMask connectionPointLayer;
    [SerializeField] private LineRenderer connectionLinePreview;
    [SerializeField] private Material connectionLineMaterial;

    private Node selectedNode = null;
    private ConnectionPoint draggingFromPoint = null;
    private List<Connection> activeConnections = new List<Connection>();

    private Camera cam;

    protected override void Awake()
    {
        base.Awake();

        cam = Camera.main;
        connectionLinePreview.enabled = false;
    }
    private void Update()
    {
        Vector3 mouseWorld = cam.ScreenToWorldPoint(InputHandler.Instance.MousePosition);
        mouseWorld.z = 0f;

        if(InputHandler.Instance.MouseLeftClickHold)
        {
            if(selectedNode != null)
            {
                selectedNode.transform.position = SnapToGrid(mouseWorld);
            }
            else if(draggingFromPoint != null)
            {
                DrawConnectionPreview(draggingFromPoint.transform.position, mouseWorld);
            }
        }
        if(InputHandler.Instance.MouseLeftClickPressed)
        {
            TryStartDraggingOrConnecting(mouseWorld);
        }
        if(InputHandler.Instance.MouseLeftClickReleased)
        {
            if(selectedNode != null)
            {
                selectedNode = null;
            }

            if(draggingFromPoint != null)
            {
                TryCompleteConnection(mouseWorld);
                draggingFromPoint = null;
                connectionLinePreview.enabled = false;
            }
        }

        foreach(var connection in activeConnections)
        {
            connection.UpdateLine();
        }
    }

    private void TryStartDraggingOrConnecting(Vector3 mouseWorld)
    {
        Collider2D connectionPointHit = Physics2D.OverlapPoint(mouseWorld, connectionPointLayer);
        if(connectionPointHit != null)
        {
            draggingFromPoint = connectionPointHit.GetComponent<ConnectionPoint>();
            connectionLinePreview.enabled = true;
            return;
        }

        Collider2D nodeHit = Physics2D.OverlapPoint(mouseWorld, nodeLayer);
        if(nodeHit != null)
        {
            selectedNode = nodeHit.GetComponent<Node>();
        }
    }
    private void TryCompleteConnection(Vector3 mouseWorld)
    {
        Collider2D connectionPointHit = Physics2D.OverlapPoint(mouseWorld, connectionPointLayer);
        if(connectionPointHit == null) return;

        ConnectionPoint targetPoint = connectionPointHit.GetComponent<ConnectionPoint>();
        if(targetPoint == null || targetPoint == draggingFromPoint) return;

        if(draggingFromPoint.ConnectionType == targetPoint.ConnectionType) return;

        CreateConnectionLine(draggingFromPoint, targetPoint);
    }
    private void CreateConnectionLine(ConnectionPoint from, ConnectionPoint to)
    {
        GameObject lineObj = new GameObject("ConnectionLine");
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.material = connectionLineMaterial;
        lr.useWorldSpace = true;
        lr.SetPosition(0, from.transform.position);
        lr.SetPosition(1, to.transform.position);

        CreateConnection(draggingFromPoint, to, lr);
    }
    private void CreateConnection(ConnectionPoint from, ConnectionPoint to, LineRenderer lr)
    {
        Connection connection = new Connection(from.ParentNode, to.ParentNode, from, to, lr);
        from.AddConnection(connection);
        to.AddConnection(connection);

        activeConnections.Add(connection);

        connection.UpdateLine();
    }
    private void DrawConnectionPreview(Vector3 from, Vector3 to)
    {
        connectionLinePreview.enabled = true;
        connectionLinePreview.positionCount = 2;
        connectionLinePreview.SetPosition(0, from);
        connectionLinePreview.SetPosition(1, to);
    }
    private Vector3 SnapToGrid(Vector3 position)
    {
        float cellSize = GridDrawer.Instance.CellSize;
        float x = Mathf.Round(position.x / cellSize) * cellSize;
        float y = Mathf.Round(position.y / cellSize) * cellSize;
        return new Vector3(x, y, 0f);
    }
}
