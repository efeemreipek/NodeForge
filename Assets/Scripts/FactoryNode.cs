using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class FactoryNode : Node
{
    public Recipe Recipe;
    public int StorageCapacity = 100;
    [SerializedDictionary("Input Resource", "Amount")]
    public SerializedDictionary<Resource, int> InputStorage = new SerializedDictionary<Resource, int>();
    [SerializedDictionary("Output Resource", "Amount")]
    public SerializedDictionary<Resource, int> OutputStorage = new SerializedDictionary<Resource, int>();

    private float processTimer = 0f;
    private float transferTimer = 0f;
    private enum State { Idle, Processing, Transferring };
    private State currentState = State.Idle;
    private FactoryNodeUI ui;

    private void Awake()
    {
        ui = GetComponent<FactoryNodeUI>();
    }
    private void Start()
    {
        for(int i = 0; i < InputPoints.Count; i++)
        {
            InputPoints[i].InitializeConnectionPoint(Recipe.Inputs[i].Resource);
        }
        for(int i = 0; i < OutputPoints.Count; i++)
        {
            OutputPoints[i].InitializeConnectionPoint(Recipe.Outputs[i].Resource);
        }

        InitializeStorages();
        ui.UpdateProgressBar(0f);
    }
    private void Update()
    {
        if(!IsActive) return;

        switch(currentState)
        {
            case State.Idle:
                if(CanTransfer())
                {
                    currentState = State.Transferring;
                    transferTimer = 0f;
                    ui.UpdateProgressBar(0f);
                }
                else if(CanProcess())
                {
                    currentState = State.Processing;
                    processTimer = 0f;
                    ui.UpdateProgressBar(0f);
                }
                break;

            case State.Processing:
                if(processTimer < Recipe.ProductionTime)
                {
                    processTimer += Time.deltaTime;
                    ui.UpdateProgressBar(processTimer / Recipe.ProductionTime);
                }
                else
                {
                    ProcessResource();
                    currentState = State.Idle;
                    ui.UpdateProgressBar(0f);
                }
                break;

            case State.Transferring:
                if(transferTimer < 1f)
                {
                    transferTimer += Time.deltaTime;
                    ui.UpdateProgressBar(transferTimer / 1f);
                }
                else
                {
                    TransferResource(Recipe.Outputs);
                    currentState = State.Idle;
                    ui.UpdateProgressBar(0f);
                }
                break;
        }
    }
    private bool CanProcess()
    {
        foreach(var input in Recipe.Inputs)
        {
            if(InputStorage[input.Resource] < input.Amount) return false;
        }
        foreach(var output in Recipe.Outputs)
        {
            if(OutputStorage[output.Resource] + output.Amount > StorageCapacity) return false;
        }
        return true;
    }
    private bool CanTransfer()
    {
        if(!HasOutputConnections()) return false;
        foreach(var output in Recipe.Outputs)
        {
            if(OutputStorage[output.Resource] > 0) return true;
        }
        return false;
    }

    private void InitializeStorages()
    {
        foreach(ResourceAmount ra in Recipe.Inputs)
        {
            InputStorage.Add(ra.Resource, 0);
        }
        foreach(ResourceAmount ra in Recipe.Outputs)
        {
            OutputStorage.Add(ra.Resource, 0);
        }
    }
    private void ProcessResource()
    {
        foreach(ResourceAmount ra in Recipe.Inputs)
        {
            InputStorage[ra.Resource] -= ra.Amount;
        }
        foreach(ResourceAmount ra in Recipe.Outputs)
        {
            OutputStorage[ra.Resource] += ra.Amount;
        }
    }
    private void TransferResource(List<ResourceAmount> resources)
    {
        foreach(ConnectionPoint outputPoint in OutputPoints)
        {
            foreach(ResourceAmount ra in resources)
            {
                outputPoint.Connection.TransferResource(ra.Resource, 1);
                OutputStorage[ra.Resource] -= ra.Amount;
            }
        }
    }
    public void AcceptResource(Resource resource, int amount)
    {
        if(InputStorage.ContainsKey(resource))
        {
            InputStorage[resource] += amount;
        }
        else
        {
            InputStorage[resource] = amount;
        }
    }
}
