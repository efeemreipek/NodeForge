using System;
using UnityEngine;

public class NodeBuildButton : MonoBehaviour
{
    private NodeBuildButtonUI ui;

    private void Awake()
    {
        ui = GetComponent<NodeBuildButtonUI>();
    }

    public void InitializeButton(string buttonName, GameObject prefab)
    {
        ui.InitializeUI(buttonName, () => BuildNode(prefab)) ;
    }
    public void BuildNode(GameObject prefab)
    {
        GameObject nodeGO = Instantiate(prefab);
    }
}
