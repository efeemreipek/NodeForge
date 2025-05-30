using System;
using UnityEngine;

public class NodeBuildButton : MonoBehaviour
{
    private NodeBuildButtonUI ui;

    private void Awake()
    {
        ui = GetComponent<NodeBuildButtonUI>();
    }

    public void InitializeButton(string buttonName)
    {
        ui.InitializeUI(buttonName, () => Debug.Log(buttonName)) ;
    }
}
