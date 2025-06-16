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
    [SerializeField] private Connection connection;

    private Node parentNode;
    private ConnectionPointUI ui;

    public ConnectionType ConnectionType => connectionType;
    public Node ParentNode => parentNode;
    public Connection Connection => connection;
    public Resource Resource => resource;

    private void Awake()
    {
        ui = GetComponent<ConnectionPointUI>();
        parentNode = GetComponentInParent<Node>();
    }

    public void InitializeConnectionPoint(Resource resource)
    {
        this.resource = resource;
        ui.InitializeUI(this.resource);

        connection = null;
    }
    public void InitializeConnectionPoint()
    {
        ui.InitializeUI();

        connection = null;
    }
    public void AddConnection(Connection connection)
    {
        this.connection = connection;
    }
    public void ClearConnection()
    {
        if(connection != null)
        {
            if(connection.InputPoint != null && connection.InputPoint != this)
            {
                connection.InputPoint.connection = null;
            }

            if(connection.OutputPoint != null && connection.OutputPoint != this)
            {
                connection.OutputPoint.connection = null;
            }

            if(connection.ConnectionLine != null && connection.ConnectionLine.gameObject != null)
            {
                Destroy(connection.ConnectionLine.gameObject);
            }

            connection = null;
        }
    }
}
