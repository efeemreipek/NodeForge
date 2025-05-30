using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Node Data List", menuName = "Scriptable Objects/Node Data List")]
public class NodeDataList : ScriptableObject
{
    public List<NodeData> Nodes = new List<NodeData>();
}
