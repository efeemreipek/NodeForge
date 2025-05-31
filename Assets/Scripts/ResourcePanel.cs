using UnityEngine;

public class ResourcePanel : MonoBehaviour
{
    [SerializeField] private ResourceList resourceList;
    [SerializeField] private GameObject resourceUIPrefab;

    private void Awake()
    {
        for(int i = 0; i < resourceList.Resources.Count; i++)
        {
            GameObject resourceUIGO = Instantiate(resourceUIPrefab, transform);
            ResourceUI ui = resourceUIGO.GetComponent<ResourceUI>();
            Resource resource = resourceList.Resources[i];
            ui.InitializeUI(resource, resource.Icon, ResourceManager.Instance.GetResourceAmount(resource));
        }
    }
}
