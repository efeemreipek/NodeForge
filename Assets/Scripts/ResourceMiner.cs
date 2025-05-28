using UnityEngine;

public class ResourceMiner : MonoBehaviour
{
    [SerializeField] private Resource resource;

    public void MineResource()
    {
        ResourceManager.Instance.AddResource(resource, 1);
    }
}
