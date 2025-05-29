using UnityEngine;

[System.Serializable]
public class Connection
{
    public Node FromNode;
    public Node ToNode;
    public ConnectionPoint InputPoint;
    public ConnectionPoint OutputPoint;
    public float TransferBuffer = 0f;

    private LineRenderer connectionLine;

    public Connection(Node fromNode, Node toNode, ConnectionPoint inputPoint, ConnectionPoint outputPoint, LineRenderer connectionLine)
    {
        FromNode = fromNode;
        ToNode = toNode;
        InputPoint = inputPoint;
        OutputPoint = outputPoint;
        this.connectionLine = connectionLine;
    }

    public void UpdateLine()
    {
        if(connectionLine == null || InputPoint == null || OutputPoint == null) return;

        connectionLine.SetPosition(0, OutputPoint.transform.position);
        connectionLine.SetPosition(1, InputPoint.transform.position);
    }
}
