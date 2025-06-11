using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializedDictionary("Resource", "Amount")]
    public SerializedDictionary<Resource, int> resources = new SerializedDictionary<Resource, int>();

    public event Action<Resource, int> OnResourceAmountChanged;
    public event Action OnInventoryChanged;

    public void AddResource(Resource resource, int amount)
    {
        if(resources.ContainsKey(resource))
        {
            resources[resource] += amount;
        }
        else
        {
            resources[resource] = amount;
        }

        OnResourceAmountChanged.Invoke(resource, GetResourceAmount(resource));
        OnInventoryChanged?.Invoke();
    }
    public bool HasEnoughResources(Resource resource, int amount)
    {
        if(!resources.ContainsKey(resource) || resources[resource] < amount)
        {
            return false;
        }
        return true;
    }
    public void ConsumeResources(Resource resource, int amount)
    {
        resources[resource] -= amount;
        OnResourceAmountChanged.Invoke(resource, GetResourceAmount(resource));
        OnInventoryChanged?.Invoke();
    }
    public int GetResourceAmount(Resource resource)
    {
        return resources.ContainsKey(resource) ? resources[resource] : 0;
    }
}
