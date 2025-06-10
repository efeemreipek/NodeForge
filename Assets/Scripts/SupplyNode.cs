using System;
using UnityEngine;

public class SupplyNode : Node
{
    public Resource Resource;
    public float TransferTime = 1f;

    private float transferTimer = 0f;

    private SupplyNodeUI ui;

    private void Awake()
    {
        ui = GetComponent<SupplyNodeUI>();
    }
    private void Start()
    {
        foreach(ConnectionPoint outputPoint in OutputPoints)
        {
            outputPoint.InitializeConnectionPoint(Resource);
        }

        ui.UpdateProgressBar(0f);
    }
    private void Update()
    {
        if(!IsActive) return;
        if(!ResourceManager.Instance.HasEnoughResources(Resource, 1)) return;
        if(!HasOutputConnections()) return;

        if(transferTimer < TransferTime)
        {
            transferTimer += Time.deltaTime;
            ui.UpdateProgressBar(transferTimer / TransferTime);
            return;
        }
        transferTimer = 0f;

        TransferResource(Resource, 1);

        ui.UpdateProgressBar(0f);
    }

    private void TransferResource(Resource resource, int amount)
    {
        foreach(ConnectionPoint outputPoint in OutputPoints)
        {
            outputPoint.Connection.TransferResource(resource, amount);
            ResourceManager.Instance.ConsumeResources(resource, amount);
        }
    }
}
