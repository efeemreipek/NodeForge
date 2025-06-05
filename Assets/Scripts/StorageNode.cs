public class StorageNode : Node
{
    private void Start()
    {
        foreach(ConnectionPoint inputPoint in InputPoints)
        {
            inputPoint.InitializeConnectionPoint();
        }
    }

    public void AcceptResource(Resource resource, int amount)
    {
        ResourceManager.Instance.AddResource(resource, amount);
    }
}
