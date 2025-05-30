using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : Singleton<ConnectionManager>
{
    [SerializeField] private LayerMask connectionPointLayer;
    [SerializeField] private LineRenderer connectionLinePreview;
    [SerializeField] private Material connectionLineMaterial;

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

        if(InputHandler.Instance.MouseLeftClickHold && draggingFromPoint != null)
        {
            DrawConnectionPreview(draggingFromPoint.transform.position, mouseWorld);
        }
        if(InputHandler.Instance.MouseLeftClickPressed)
        {
            TryStartConnection(mouseWorld);
        }
        if(InputHandler.Instance.MouseLeftClickReleased)
        {
            TryCompleteConnection(mouseWorld);
            draggingFromPoint = null;
            connectionLinePreview.enabled = false;
        }

        foreach(var connection in activeConnections)
        {
            connection.UpdateLine();
        }
    }

    private void TryStartConnection(Vector3 mouseWorld)
    {
        Collider2D hit = Physics2D.OverlapPoint(mouseWorld, connectionPointLayer);
        if(hit != null)
        {
            draggingFromPoint = hit.GetComponent<ConnectionPoint>();
            connectionLinePreview.enabled = true;
        }
    }
    private void TryCompleteConnection(Vector3 mouseWorld)
    {
        if(draggingFromPoint == null) return;

        Collider2D hit = Physics2D.OverlapPoint(mouseWorld, connectionPointLayer);
        if(hit == null) return;

        ConnectionPoint targetPoint = hit.GetComponent<ConnectionPoint>();
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
        lr.sortingOrder = -1;
        lr.SetPosition(0, from.transform.position);
        lr.SetPosition(1, to.transform.position);

        CreateConnection(from, to, lr);
    }

    private void CreateConnection(ConnectionPoint from, ConnectionPoint to, LineRenderer lr)
    {
        Connection connection;
        if(from.ConnectionType == ConnectionType.Input) // if start connection point is input change node order
        {
            // Input to output
            connection = new Connection(to.ParentNode, from.ParentNode, from, to, lr);
        }
        else
        {
            // Output to input
            connection = new Connection(from.ParentNode, to.ParentNode, to, from, lr);
        }

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
}
