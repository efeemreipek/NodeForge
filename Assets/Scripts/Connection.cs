public class Connection
{
    public Node FromNode;
    public Node ToNode;
    public ConnectionPoint InputPoint;
    public ConnectionPoint OutputPoint;
    public float TransferBuffer = 0f;

    public Connection(Node fromNode, Node toNode, ConnectionPoint ýnputPoint, ConnectionPoint outputPoint)
    {
        FromNode = fromNode;
        ToNode = toNode;
        InputPoint = ýnputPoint;
        OutputPoint = outputPoint;
    }
}
