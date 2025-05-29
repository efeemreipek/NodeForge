using UnityEngine;

public class SupplyNode : Node
{
    public Resource Resource;

    private void Awake()
    {
        foreach(ConnectionPoint outputPoint in OutputPoints)
        {
            outputPoint.InitializeConnectionPoint(Resource);
        }
    }
}
