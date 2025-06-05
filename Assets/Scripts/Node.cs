using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public GameObject SelectedVisualGO;

    public List<ConnectionPoint> InputPoints = new List<ConnectionPoint>();
    public List<ConnectionPoint> OutputPoints = new List<ConnectionPoint>();

    public bool IsActive = true;

    private void OnDestroy()
    {
        foreach(var connectionPoint in InputPoints)
        {
            connectionPoint.DeleteConnections();
        }
        foreach(var connectionPoint in OutputPoints)
        {
            connectionPoint.DeleteConnections();
        }
    }
    protected bool HasConnections()
    {
        return HasInputConnections() || HasOutputConnections();
    }
    protected bool HasInputConnections()
    {
        foreach(var connectionPoint in InputPoints)
        {
            if(connectionPoint.Connections.Count == 0) continue;
            else return true;
        }

        return false;
    }
    protected bool HasOutputConnections()
    {
        foreach(var connectionPoint in OutputPoints)
        {
            if(connectionPoint.Connections.Count == 0) continue;
            else return true;
        }

        return false;
    }
}
