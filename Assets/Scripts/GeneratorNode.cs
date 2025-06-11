using UnityEngine;

public class GeneratorNode : Node
{
    public Resource Resource;
    public float GenerateTime = 3f;

    private float generateTimer = 0f;

    private GeneratorNodeUI ui;

    private void Awake()
    {
        ui = GetComponent<GeneratorNodeUI>();
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
        if(!HasOutputConnections()) return;

        if(generateTimer < GenerateTime)
        {
            generateTimer += Time.deltaTime;
            ui.UpdateProgressBar(generateTimer / GenerateTime);
            return;
        }
        generateTimer = 0f;

        TransferResource(Resource, 1);

        ui.UpdateProgressBar(0f);
    }

    private void TransferResource(Resource resource, int amount)
    {
        foreach(ConnectionPoint outputPoint in OutputPoints)
        {
            outputPoint.Connection.TransferResource(resource, amount);
        }
    }
}
