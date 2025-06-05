[System.Serializable]
public class ResourceAmount
{
    public Resource Resource;
    public int Amount;

    public ResourceAmount(Resource resource, int amount)
    {
        Resource = resource;
        Amount = amount;
    }
}
