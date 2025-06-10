using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : Singleton<ConnectionManager>
{
    [SerializeField] private LayerMask connectionPointLayer;
    [SerializeField] private LayerMask connectionLayer;
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
        if(InputHandler.Instance.MouseRightClickPressed)
        {
            DeleteConnection(mouseWorld);
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

        if(draggingFromPoint.Connection != null)
        {
            RemoveConnection(draggingFromPoint.Connection);
        }
        if(targetPoint.Connection != null)
        {
            RemoveConnection(targetPoint.Connection);
        }


        CreateConnectionLine(draggingFromPoint, targetPoint);
    }
    private void CreateConnectionLine(ConnectionPoint from, ConnectionPoint to)
    {
        GameObject lineObj = new GameObject("ConnectionLine");
        lineObj.layer = Mathf.RoundToInt(Mathf.Log(connectionLayer.value, 2));
        ConnectionMono cm = lineObj.AddComponent<ConnectionMono>();
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 4;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.material = connectionLineMaterial;
        lr.useWorldSpace = true;
        lr.sortingOrder = -1;

        Vector3 point0 = from.transform.position;
        Vector3 point3 = to.transform.position;
        Vector3 point1 = new Vector3(point0.x + 0.5f, point0.y, 0f);
        Vector3 point2 = new Vector3(point3.x - 0.5f, point3.y, 0f);

        lr.SetPosition(0, point0);
        lr.SetPosition(1, point1);
        lr.SetPosition(2, point2);
        lr.SetPosition(3, point3);

        EdgeCollider2D edge = lineObj.AddComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[4];
        points[0] = point0;
        points[1] = point1;
        points[2] = point2;
        points[3] = point3;

        edge.points = points;
        edge.edgeRadius = 0.05f;

        CreateConnection(from, to, lr, cm);
    }
    private void CreateConnection(ConnectionPoint from, ConnectionPoint to, LineRenderer lr, ConnectionMono cm)
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
        cm.Connection = connection;

        connection.UpdateLine();
    }
    private void DeleteConnection(Vector3 mouseWorld)
    {
        Collider2D hit = Physics2D.OverlapPoint(mouseWorld, connectionLayer);
        if(hit != null)
        {
            GameObject connectionGO = hit.gameObject;
            ConnectionMono connectionMono = connectionGO.GetComponent<ConnectionMono>();

            RemoveConnection(connectionMono.Connection);
        }
    }
    private void RemoveConnection(Connection connection)
    {
        if(connection.InputPoint != null)
        {
            connection.InputPoint.ClearConnection();
        }

        if(connection.OutputPoint != null)
        {
            connection.OutputPoint.ClearConnection();
        }

        if(connection.ConnectionLine != null && connection.ConnectionLine.gameObject != null)
        {
            Destroy(connection.ConnectionLine.gameObject);
        }

        activeConnections.Remove(connection);
    }
    private void DrawConnectionPreview(Vector3 from, Vector3 to)
    {
        connectionLinePreview.enabled = true;
        connectionLinePreview.positionCount = 4;

        Vector3 point0 = from;
        Vector3 point3 = to;
        Vector3 point1 = new Vector3(point0.x + 0.5f, point0.y, 0f);
        Vector3 point2 = new Vector3(point3.x - 0.5f, point3.y, 0f);

        connectionLinePreview.SetPosition(0, point0);
        connectionLinePreview.SetPosition(1, point1);
        connectionLinePreview.SetPosition(2, point2);
        connectionLinePreview.SetPosition(3, point3);
    }
}
