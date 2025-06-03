using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public GameObject SelectedVisualGO;

    public List<ConnectionPoint> InputPoints = new List<ConnectionPoint>();
    public List<ConnectionPoint> OutputPoints = new List<ConnectionPoint>();

    public bool IsActive = true;
    public float StorageCapacity = 100f;

    protected Dictionary<Resource, float> inputStorage = new Dictionary<Resource, float>();
    protected Dictionary<Resource, float> outputStorage = new Dictionary<Resource, float>();

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
}
