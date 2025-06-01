using UnityEngine;

[System.Serializable]
public class Connection
{
    public Node FromNode;
    public Node ToNode;
    public ConnectionPoint InputPoint;
    public ConnectionPoint OutputPoint;
    public float TransferBuffer = 0f;
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

        ConnectionLine.SetPosition(0, OutputPoint.transform.position);
        ConnectionLine.SetPosition(1, InputPoint.transform.position);
    }
}
