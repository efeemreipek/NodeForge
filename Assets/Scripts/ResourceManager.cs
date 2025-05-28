using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializedDictionary("Resource", "Amount")]
    public SerializedDictionary<Resource, float> resources = new SerializedDictionary<Resource, float>();

    public void AddResource(Resource resource, float amount)
    {
        if(resources.ContainsKey(resource))
        {
            resources[resource] += amount;
        }
        else
        {
            resources[resource] = amount;
        }
    }
    public bool HasEnoughResources(List<ResourceAmount> required)
    {
        foreach(var req in required)
        {
            if(!resources.ContainsKey(req.Resource) || resources[req.Resource] < req.Amount)
            {
                return false;
            }
        }
        return true;
    }
    public void ConsumeResources(List<ResourceAmount> required)
    {
        foreach(var req in required)
        {
            resources[req.Resource] -= req.Amount;
        }
    }
    public float GetResourceAmount(Resource resource)
    {
        return resources.ContainsKey(resource) ? resources[resource] : 0f;
    }
}
