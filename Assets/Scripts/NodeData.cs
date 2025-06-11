using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Node Data", menuName = "Scriptable Objects/Node Data")]
public class NodeData : ScriptableObject
{
    public string Name;
    public GameObject Prefab;
    public bool HasBuildRequirements;
    public List<ResourceAmount> BuildRequirements = new List<ResourceAmount>();
}
