using System;
using System.Collections.Generic;
using UnityEngine;

public enum ConnectionType
{
    Input,
    Output
}

public class ConnectionPoint : MonoBehaviour
{
    [SerializeField] private Resource resource;
    [SerializeField] private ConnectionType connectionType;
    [SerializeField] private float transferRate = 1f;
    [SerializeField] private List<Connection> connections = new List<Connection>();

    private ConnectionPointUI ui;

    private void Awake()
    {
        ui = GetComponent<ConnectionPointUI>();
    }

    public void InitializeConnectionPoint(Resource resource)
    {
        this.resource = resource;
        ui.InitializeUI(this.resource);
    }
}
