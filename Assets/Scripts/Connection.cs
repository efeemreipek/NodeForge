using UnityEngine;

[System.Serializable]
public class Connection
{
    public Node FromNode;
    public Node ToNode;
    public ConnectionPoint InputPoint;
    public ConnectionPoint OutputPoint;
    public LineRenderer ConnectionLine;


    public Connection(Node fromNode, Node toNode, ConnectionPoint inputPoint, ConnectionPoint outputPoint, LineRenderer connectionLine)
    {
        FromNode = fromNode;
        ToNode = toNode;
        InputPoint = inputPoint;
        OutputPoint = outputPoint;
        ConnectionLine = connectionLine;
    }

    public void UpdateLine()
    {
        if(ConnectionLine == null || InputPoint == null || OutputPoint == null) return;

        Vector3 from = OutputPoint.transform.position;
        Vector3 to = InputPoint.transform.position;

        ConnectionLine.positionCount = 4;

        Vector3 point0 = from;
        Vector3 point3 = to;
        Vector3 point1 = new Vector3(point0.x + 0.5f, point0.y, 0f);
        Vector3 point2 = new Vector3(point3.x - 0.5f, point3.y, 0f);

        ConnectionLine.SetPosition(0, point0);
        ConnectionLine.SetPosition(1, point1);
        ConnectionLine.SetPosition(2, point2);
        ConnectionLine.SetPosition(3, point3);

        EdgeCollider2D edgeCollider = ConnectionLine.GetComponent<EdgeCollider2D>();
        if(edgeCollider != null)
        {
            Vector2[] colliderPoints = new Vector2[4];
            colliderPoints[0] = point0;
            colliderPoints[1] = point1;
            colliderPoints[2] = point2;
            colliderPoints[3] = point3;
            edgeCollider.points = colliderPoints;
        }
    }
    public void TransferResource(Resource resource, int amount)
    {
        if(ToNode is StorageNode)
        {
            StorageNode node = (StorageNode)ToNode;
            node.AcceptResource(resource, amount);
        }
        else
        {
            FactoryNode node = (FactoryNode)ToNode;
            node.AcceptResource(resource, amount);
        }
    }
}
