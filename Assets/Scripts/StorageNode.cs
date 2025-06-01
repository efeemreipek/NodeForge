using UnityEngine;

public class StorageNode : Node
{
    private void Start()
    {
        foreach(ConnectionPoint inputPoint in InputPoints)
        {
            inputPoint.InitializeConnectionPoint();
        }
    }
}
