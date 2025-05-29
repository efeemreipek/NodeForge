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

    private Node parentNode;
    private ConnectionPointUI ui;

    public ConnectionType ConnectionType => connectionType;
    public Node ParentNode => parentNode;

    private void Awake()
    {
        ui = GetComponent<ConnectionPointUI>();
        parentNode = GetComponentInParent<Node>();
    }

    public void InitializeConnectionPoint(Resource resource)
    {
        this.resource = resource;
        ui.InitializeUI(this.resource);
    }
    public void AddConnection(Connection connection)
    {
        connections.Add(connection);
    }
}
