using System.Collections.Generic;

public enum ConnectionType
{
    Input,
    Output
}

[System.Serializable]
public class ConnectionPoint
{
    public Resource Resource;
    public ConnectionType ConnectionType;
    public float TransferRate = 1f;
    public List<Connection> Connections = new List<Connection>();
}
