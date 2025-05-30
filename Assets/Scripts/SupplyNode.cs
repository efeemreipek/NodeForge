public class SupplyNode : Node
{
    public Resource Resource;

    private void Start()
    {
        foreach(ConnectionPoint outputPoint in OutputPoints)
        {
            outputPoint.InitializeConnectionPoint(Resource);
        }
    }
}
