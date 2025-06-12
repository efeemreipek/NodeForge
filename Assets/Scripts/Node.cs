using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Node : MonoBehaviour
{
    public GameObject SelectedVisualGO;

    public List<ConnectionPoint> InputPoints = new List<ConnectionPoint>();
    public List<ConnectionPoint> OutputPoints = new List<ConnectionPoint>();

    public bool IsActive = true;

    protected SortingGroup sortingGroup;
    private static int sortingOrderCounter = 0;

    protected virtual void Awake()
    {
        sortingGroup = GetComponent<SortingGroup>();
    }
    protected void OnDestroy()
    {
        foreach(var connectionPoint in InputPoints)
        {
            connectionPoint.ClearConnection();
        }
        foreach(var connectionPoint in OutputPoints)
        {
            connectionPoint.ClearConnection();
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
            if(connectionPoint.Connection == null) continue;
            else return true;
        }

        return false;
    }
    protected bool HasOutputConnections()
    {
        foreach(var connectionPoint in OutputPoints)
        {
            if(connectionPoint.Connection == null) continue;
            else return true;
        }

        return false;
    }

    public void BringToFront()
    {
        sortingOrderCounter++;
        if(sortingGroup != null)
        {
            sortingGroup.sortingOrder = sortingOrderCounter;
        }
    }
    public void ResetSortingOrder(int order)
    {
        if(sortingGroup != null)
        {
            sortingGroup.sortingOrder = order;
        }
    }
}
