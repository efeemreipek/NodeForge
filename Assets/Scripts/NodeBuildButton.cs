using System;
using System.Collections.Generic;
using UnityEngine;

public class NodeBuildButton : MonoBehaviour
{
    private bool hasBuildRequirements;
    private List<ResourceAmount> buildRequirements;

    private NodeBuildButtonUI ui;

    private void Awake()
    {
        ui = GetComponent<NodeBuildButtonUI>();
    }
    private void OnEnable()
    {
        ResourceManager.Instance.OnInventoryChanged += UpdateButton;
    }
    private void OnDisable()
    {
        if(ResourceManager.HasInstance) ResourceManager.Instance.OnInventoryChanged -= UpdateButton;
    }
    private void Start()
    {
        UpdateButton();
    }

    private void UpdateButton()
    {
        if(!hasBuildRequirements) return;

        bool hasEnoughResources = true;
        foreach(ResourceAmount requirement in buildRequirements)
        {
            if(ResourceManager.Instance.HasEnoughResources(requirement.Resource, requirement.Amount))
            {
                hasEnoughResources = true;
            }
            else
            {
                hasEnoughResources = false;
                break;
            }
        }
        ui.SetButtonEnabled(hasEnoughResources);
    }

    public void InitializeButton(string buttonName, GameObject prefab)
    {
        ui.InitializeUI(buttonName, () => BuildNode(prefab)) ;
    }
    public void BuildNode(GameObject prefab)
    {
        GameObject nodeGO = Instantiate(prefab);

        if(hasBuildRequirements)
        {
            foreach(ResourceAmount requirement in buildRequirements)
            {
                ResourceManager.Instance.ConsumeResources(requirement.Resource, requirement.Amount);
            }
        }
    }
    public void InitializeRequirements(bool hasRequirements, List<ResourceAmount> buildRequirements)
    {
        hasBuildRequirements = hasRequirements;
        if(hasBuildRequirements)
        {
            this.buildRequirements = buildRequirements;
        }
    }
}
